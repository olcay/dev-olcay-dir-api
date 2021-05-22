using System;

namespace WebApi.Models
{
    public class MessageBoxDto
    {
        public Guid Id { get; set; }

        public Guid PetId { get; set; }
        
        public string PetTitle { get; set; }

        public bool IsRead { get; set; }
    }
}
