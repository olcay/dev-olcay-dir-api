using WebApi.Models;
using System.ComponentModel.DataAnnotations;
using WebApi.Persistence.Services;

namespace WebApi.ValidationAttributes
{
    public class RaceMustBeExistingAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var raceId = value as int?;

            var repository = (IRepository)validationContext
                        .GetService(typeof(IRepository));

            if (raceId.HasValue && !repository.RaceExists(raceId.Value))
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(PetForManipulationDto.RaceId) });
            }

            return ValidationResult.Success;
        }
    }
}
