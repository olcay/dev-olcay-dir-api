using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Action
    {
        [Key]
        public Guid Id { get; set; }

        public ActionType ActionType { get; set; }

        [MaxLength(100)]
        public string IpAddress { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        [ForeignKey("PetId")]
        public Pet Pet { get; set; }

        public Guid PetId { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
    }

    public enum ActionType
    {
        View,
        Share,
        Flag
    }
}