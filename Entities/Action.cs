using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Action
    {
        public Action()
        {
            CreationDate = DateTimeOffset.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public ActionType ActionType { get; set; }

        [MaxLength(100)]
        public string IpAddress { get; set; }

        [Required]
        public DateTimeOffset CreationDate { get; set; }

        [ForeignKey("PetId")]
        public Pet Pet { get; set; }

        public Guid PetId { get; set; }
    }

    public enum ActionType
    {
        View,
        Share
    }
}