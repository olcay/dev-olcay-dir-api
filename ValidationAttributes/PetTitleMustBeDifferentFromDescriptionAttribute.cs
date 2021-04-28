using WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ValidationAttributes
{
    public class PetTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var item = (PetForManipulationDto)validationContext.ObjectInstance;

            if (item.Title == item.Description)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(PetForManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
