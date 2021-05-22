using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class MessageBoxParticipant
    {
        [Required]
        public DateTimeOffset? Read { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("MessageBoxId")]
        public MessageBox MessageBox { get; set; }

        public Guid MessageBoxId { get; set; }

        public bool IsDeleted { get; set; }

        internal void Delete()
        {
            IsDeleted = true;
        }
    }
}