using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [ForeignKey("PetId")]
        public Pet Pet { get; set; }

        public Guid PetId { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public void Create(Guid petId)
        {
            Id = Guid.NewGuid();
            PetId = petId;
            Created = DateTimeOffset.UtcNow;
        }

        public string FileName() => string.Concat(PetId, "/", Id, ".image");
    }
}