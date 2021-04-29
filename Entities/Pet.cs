using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Enums;

namespace WebApi.Entities
{
    public class Pet
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public PetType PetType { get; set; }

        [Required]
        public PetStatus PetStatus { get; set; }

        public PetAge Age { get; set; }

        public Gender Gender { get; set; }

        public Size Size { get; set; }

        public FromWhere FromWhere { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        [ForeignKey("RaceId")]
        public Race Race { get; set; }

        public int? RaceId { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CreatedById")]
        public Account CreatedBy { get; set; }

        public int CreatedById { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        [ForeignKey("AdoptedById")]
        public Account AdoptedBy { get; set; }

        public int? AdoptedById { get; set; }

        public DateTimeOffset? Published { get; set; }

        public void Publish()
        {
            Published = DateTimeOffset.UtcNow;
            PetStatus = PetStatus.Published;
        }

        public void Create(int accountId)
        {
            CreatedById = accountId;
            Created = DateTimeOffset.UtcNow;
            PetStatus = PetStatus.Created;
        }

        public void Adopt(int? accountId)
        {
            AdoptedById = accountId;
            PetStatus = PetStatus.Adopted;
        }

        public void Delete()
        {
            PetStatus = PetStatus.Deleted;
        }
    }
}