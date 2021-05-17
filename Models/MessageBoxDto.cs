using System;
using System.Collections.Generic;
using WebApi.Models.Accounts;

namespace WebApi.Models
{
    public class MessageBoxDto
    {
        public Guid Id { get; set; }

        public Guid PetId { get; set; }
        
        public string PetTitle { get; set; }
    }

    public class MessageDto
    {
        public Guid Id { get; set; }

        public AccountDto CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Read { get; set; }
        
        public string Body { get; set; }
    }
}
