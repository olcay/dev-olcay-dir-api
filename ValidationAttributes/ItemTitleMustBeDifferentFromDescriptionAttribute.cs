using WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ValidationAttributes
{
    public class ItemTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var item = (ItemForManipulationDto)validationContext.ObjectInstance;

            if (item.Title == item.Description)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(ItemForManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
