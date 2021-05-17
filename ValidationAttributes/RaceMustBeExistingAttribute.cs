using WebApi.Models;
using System.ComponentModel.DataAnnotations;
using WebApi.Persistence.Repositories;
using WebApi.Persistence;

namespace WebApi.ValidationAttributes
{
    public class RaceMustBeExistingAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var raceId = value as int?;

            var unitOfWork = (IUnitOfWork)validationContext
                        .GetService(typeof(IUnitOfWork));

            if (raceId.HasValue && !unitOfWork.Races.Exists(raceId.Value))
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(PetForManipulationDto.RaceId) });
            }

            return ValidationResult.Success;
        }
    }
}
