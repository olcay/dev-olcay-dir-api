using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    
    public abstract class TagForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a name.")]
        [MaxLength(100, ErrorMessage = "The name shouldn't have more than 100 characters.")]
        public string Name { get; set; }
    }
}
