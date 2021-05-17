using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Persistence;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IAccountService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        void Register(RegisterRequest model, string origin);
        void VerifyEmail(string token);
        void ForgotPassword(ForgotPasswordRequest model, string origin);
        void ValidateResetToken(ValidateResetTokenRequest model);
        void ResetPassword(ResetPasswordRequest model);
        IEnumerable<AccountResponse> GetAll();
        Task<AccountResponse> GetById(int id);
        AccountResponse Create(CreateRequest model);
        Task<AccountResponse> Update(int id, UpdateRequest model, string origin);
        Task Delete(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = _unitOfWork.Accounts.GetByEmail(model.Email);

            if (account == null || !account.IsVerified || !BC.Verify(model.Password, account.PasswordHash))
                throw new AppException("Email or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(account);
            var refreshToken = generateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from account
            removeOldRefreshTokens(account);

            // save changes to db
            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var (refreshToken, account) = getRefreshToken(token);

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTimeOffset.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();

            // generate new jwt
            var jwtToken = generateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var (refreshToken, account) = getRefreshToken(token);

            // revoke token and save
            refreshToken.Revoked = DateTimeOffset.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();
        }

        public void Register(RegisterRequest model, string origin)
        {
            // validate
            if (_unitOfWork.Accounts.ExistsByEmail(model.Email))
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }

            // map model to new account object
            var account = _mapper.Map<Account>(model);

            account.Role = Role.User;
            account.Created = DateTimeOffset.UtcNow;
            account.VerificationToken = randomTokenString();
            account.DisplayName = randomUsername();

            if (_unitOfWork.Accounts.ExistsByDisplayName(account.DisplayName))
                throw new AppException($"Lütfen tekrar deneyin.");

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            _unitOfWork.Accounts.Add(account);
            _unitOfWork.Complete();

            // send email
            sendVerificationEmail(account, origin);
        }

        public void VerifyEmail(string token)
        {
            var account = _unitOfWork.Accounts.GetByVerificationToken(token);

            if (account == null) throw new AppException("Verification failed");

            account.Verified = DateTimeOffset.UtcNow;
            account.VerificationToken = null;

            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();
        }

        public void ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = _unitOfWork.Accounts.GetByEmail(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            // create reset token that expires after 1 day
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTimeOffset.UtcNow.AddDays(1);

            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();

            // send email
            sendPasswordResetEmail(account, origin);
        }

        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            _unitOfWork.Accounts.GetByResetToken(model.Token);
        }

        public void ResetPassword(ResetPasswordRequest model)
        {
            var account = _unitOfWork.Accounts.GetByResetToken(model.Token);

            // update password and remove reset token
            account.PasswordHash = BC.HashPassword(model.Password);
            account.PasswordReset = DateTimeOffset.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();
        }

        public IEnumerable<AccountResponse> GetAll()
        {
            var accounts = _unitOfWork.Accounts.GetAll();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await _unitOfWork.Accounts.GetById(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public AccountResponse Create(CreateRequest model)
        {
            // validate
            if (_unitOfWork.Accounts.ExistsByEmail(model.Email))
                throw new AppException($"Email adresi '{model.Email}' zaten kayıtlı");

            if (_unitOfWork.Accounts.ExistsByDisplayName(model.DisplayName))
                throw new AppException($"Kullanıcı adı '{model.DisplayName}' zaten kayıtlı");

            // map model to new account object
            var account = _mapper.Map<Account>(model);
            account.Created = DateTimeOffset.UtcNow;
            account.Verified = DateTimeOffset.UtcNow;

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            _unitOfWork.Accounts.Add(account);
            _unitOfWork.Complete();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model, string origin)
        {
            var account = await _unitOfWork.Accounts.GetById(id);

            var isEmailChanged = false;

            // validate
            if (account.Email != model.Email)
            {
                if (_unitOfWork.Accounts.ExistsByEmail(model.Email))
                    throw new AppException($"Email '{model.Email}' is already taken");

                isEmailChanged = true;
                account.VerificationToken = randomTokenString();
                account.Verified = null;
            }

            if (account.DisplayName != model.DisplayName && _unitOfWork.Accounts.ExistsByDisplayName(model.DisplayName))
                throw new AppException($"Kullanıcı adı '{model.DisplayName}' zaten kayıtlı");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                account.PasswordHash = BC.HashPassword(model.Password);

            // copy model to account and save
            _mapper.Map(model, account);
            account.Updated = DateTimeOffset.UtcNow;

            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();

            if (isEmailChanged)
            {
                sendVerificationEmail(account, origin);
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
            var account = await _unitOfWork.Accounts.GetById(id);
            _unitOfWork.Accounts.Delete(account);
            _unitOfWork.Complete();
        }

        // helper methods
        
        private (RefreshToken, Account) getRefreshToken(string token)
        {
            if (token == null) throw new AppException("Invalid token");
            var account = _unitOfWork.Accounts.GetByRefreshToken(token);
            if (account == null) throw new AppException("Invalid token");
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive) throw new AppException("Invalid token");
            return (refreshToken, account);
        }

        private string generateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Created = DateTimeOffset.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private void removeOldRefreshTokens(Account account)
        {
            account.RefreshTokens.RemoveAll(x => 
                !x.IsActive && 
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTimeOffset.UtcNow);
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private string randomUsername()
        {
            var guid = Guid.NewGuid().ToString().Split("-").First();

            return "ku" + guid;
        }

        private void sendVerificationEmail(Account account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                message = $@"<p>Lütfen eposta adresinizi aşağıdaki bağlantıya tıklayarak onaylayın:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>
                             <p>Eğer bağlantı tıklanabilir değilse, kopyalayıp adres çubuğuna yapıştırarak erişebilirsiniz.</p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Eposta adresi onayı",
                html: $@"<h4>Eposta Adresinizi Onaylayınız</h4>
                         <p>Sitemize kayıt olduğunuz için teşekkürler! Sitemizden daha fazla yararlanabilmeniz için eposta adresinizin size ait olup olmadığını onaylamanız gerekiyor.</p>
                         {message}
                         <p>Bu epostanın tarafınıza yanlışlıkla ulaştığını düşünüyorsanız dikkate almayınız.</p>"
            );
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        private void sendPasswordResetEmail(Account account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }
    }
}
