using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("MessageBoxId")]
        public MessageBox MessageBox { get; set; }

        public Guid MessageBoxId { get; set; }
        
        [Required]
        public DateTimeOffset Created { get; set; }

        [ForeignKey("CreatedById")]
        public Account CreatedBy { get; set; }

        public int CreatedById { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Body { get; set; }

        public DateTimeOffset? Read { get; set; }

        public bool IsDeleted { get; set; }
    }
}