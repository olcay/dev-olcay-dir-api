using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WebApi.Entities;

namespace WebApi.Models
{
    public class PetForCreationDto : PetForManipulationDto
    {
        [Required(ErrorMessage = "Pet türü seçilmedi.")]
        [JsonProperty("PetTypeValue")]
        public PetType PetType { get; set; }
    }
}
