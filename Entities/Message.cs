using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers;

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

        public bool IsDeleted { get; set; }

        protected Message()
        { }

        public Message(string body, int accountId, MessageBox messageBox)
        {
            Body = body ?? throw new AppException(nameof(body));            
            MessageBox = messageBox ?? throw new AppException(nameof(messageBox));
            CreatedById = accountId;
            Id = Guid.NewGuid();
            Created = DateTimeOffset.UtcNow;

            messageBox.HasNewMessage();
        }

        internal void Delete()
        {
            IsDeleted = true;
        }
    }
}