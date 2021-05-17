using WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using System.Threading.Tasks;

namespace WebApi.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Account account)
        {
            _context.Accounts.Add(account);
        }

        public DbSet<Account> GetAll()
        {
            return _context.Accounts;
        }

        public async Task<Account> GetById(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public Account GetByEmail(string email)
        {
            return _context.Accounts.SingleOrDefault(x => x.Email == email);
        }

        public Account GetByRefreshToken(string token)
        {
            return _context.Accounts.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
        }

        public Account GetByVerificationToken(string token)
        {
            return _context.Accounts.SingleOrDefault(x => x.VerificationToken == token);
        }

        public Account GetByResetToken(string token)
        {
            var account = _context.Accounts.SingleOrDefault(x =>
                x.ResetToken == token &&
                x.ResetTokenExpires > DateTime.UtcNow);

            if (account == null)
                throw new AppException("Invalid token");

            return account;
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Accounts.Any(x => x.Email == email);
        }

        public bool ExistsByDisplayName(string displayName)
        {
            return _context.Accounts.Any(x => x.DisplayName == displayName);
        }

        public void Update(Account account)
        {
            _context.Update(account);
        }

        public void Delete(Account account)
        {
            _context.Accounts.Remove(account);
        }
    }
}
