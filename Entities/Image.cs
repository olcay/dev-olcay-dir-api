using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [Required]
        [StringLength(4)]
        public string FileExtension { get; set; }

        public void Create(Guid petId, string fileName)
        {
            Id = Guid.NewGuid();
            PetId = petId;
            Created = DateTimeOffset.UtcNow;
            FileExtension = fileName.Split(".").Last();
        }

        public string FileName() => string.Concat(PetId, "/", Id, ".", FileExtension);
    }
}