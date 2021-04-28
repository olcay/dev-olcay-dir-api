using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.ValidationAttributes;

namespace WebApi.Models
{
    [PetTitleMustBeDifferentFromDescription(
        ErrorMessage = "Başlık açıklamadan farklı olmalı")]
    public abstract class PetForManipulationDto
    {
        [Required(ErrorMessage = "İsim boş bırakılamaz.")]
        [MaxLength(100, ErrorMessage = "İsim 100 karakterden daha fazla olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pet türü seçilmedi.")]
        [JsonProperty("PetTypeValue")]
        public PetType PetType { get; set; }

        [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
        [MaxLength(100, ErrorMessage = "Başlık 100 karakterden daha fazla olamaz.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "Açıklama 1500 karakterden daha fazla olamaz.")]
        public string Description { get; set; }

        [JsonProperty("AgeValue")]
        public PetAge Age { get; set; }

        [JsonProperty("GenderValue")]
        public Gender Gender { get; set; }

        [JsonProperty("SizeValue")]
        public Size Size { get; set; }

        [JsonProperty("FromWhereValue")]
        public FromWhere FromWhere { get; set; }

        public int RaceId { get; set; }

        [Required(ErrorMessage = "Şehir seçilmeli.")]
        public int CityId { get; set; }

    }
}
