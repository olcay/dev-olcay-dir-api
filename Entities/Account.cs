using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool AcceptTerms { get; set; }
        public Role Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTimeOffset? Verified { get; set; }
        public bool IsVerified => (Verified.HasValue || PasswordReset.HasValue) && !Banned.HasValue;
        public string ResetToken { get; set; }
        public DateTimeOffset? ResetTokenExpires { get; set; }
        public DateTimeOffset? PasswordReset { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public DateTimeOffset? Banned { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token) 
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}