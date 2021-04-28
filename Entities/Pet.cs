using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int RaceId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        [ForeignKey("CreatedById")]
        public Account CreatedBy { get; set; }

        public int CreatedById { get; set; }

        [ForeignKey("AdoptedById")]
        public Account AdoptedBy { get; set; }

        public int? AdoptedById { get; set; }

        public DateTimeOffset? Adopted { get; set; }

        public DateTimeOffset? Published { get; set; }

        public DateTimeOffset? Deleted { get; set; }

        public bool IsAdopted => Adopted.HasValue;

        public bool IsDeleted => Deleted.HasValue;

        public bool IsPublished => Published.HasValue;
    }

    public enum PetType
    {
        Cat = 1,
        Dog = 2       
    }

    public enum PetAge
    {
        Baby = 1,
        Young = 2,
        Adult = 3,
        Old = 4
    }

    public enum Gender
    {
        Male = 2,
        Female = 1
    }

    public enum Size
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public enum FromWhere
    {
        Shelter = 1,
        Vet = 2,
        Foster = 3,
        Street = 4,
        Owner = 5
    }
}