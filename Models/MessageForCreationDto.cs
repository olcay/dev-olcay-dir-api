using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.Models
{
    public abstract class MessageForCreationDto
    {
        [Required(ErrorMessage = "Mesaj boş bırakılamaz.")]
        [MaxLength(1000, ErrorMessage = "Mesaj 1000 karakterden daha fazla olamaz.")]
        public string Body { get; set; }
    }
}
