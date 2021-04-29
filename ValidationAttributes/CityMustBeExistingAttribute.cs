using System.ComponentModel.DataAnnotations;
using WebApi.Services;

namespace WebApi.ValidationAttributes
{
    public class CityMustBeExistingAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as int?;

            return EnumService.CityExists(inputValue ?? 0);
        }
    }
}
