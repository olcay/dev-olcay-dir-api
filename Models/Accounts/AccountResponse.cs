using System;

namespace WebApi.Models.Accounts
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public DateTimeOffset? Banned { get; set; }
        public bool IsVerified { get; set; }
    }
}