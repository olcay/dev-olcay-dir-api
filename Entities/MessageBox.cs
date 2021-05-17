using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class MessageBox
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("PetId")]
        public Pet Pet { get; set; }

        public Guid PetId { get; set; }
        
        [Required]
        public DateTimeOffset Updated { get; set; }

        [ForeignKey("CreatedById")]
        public Account CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}