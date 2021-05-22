using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public ICollection<MessageBoxParticipant> MessageBoxParticipants { get; set; }

        protected MessageBox()
        { }

        internal MessageBox(int accountId, Guid petId, int petCreatedById)
        {
            CreatedById = accountId;
            PetId = petId;
            Id = Guid.NewGuid();
            Updated = DateTimeOffset.UtcNow;
            MessageBoxParticipants = new List<MessageBoxParticipant>{
                new MessageBoxParticipant() {
                    AccountId = accountId,
                    MessageBoxId = Id,
                    Read = DateTimeOffset.UtcNow
                },new MessageBoxParticipant() {
                    AccountId = petCreatedById,
                    MessageBoxId = Id,
                    Read = DateTimeOffset.UtcNow
                }
            };
        }

        internal void HasNewMessage()
        {
            Updated = DateTimeOffset.UtcNow;
        }

        

        internal void ReadBy(int accountId)
        {
            MessageBoxParticipants.SingleOrDefault(p => p.AccountId == accountId).Read = DateTimeOffset.UtcNow;
        }
    }
}