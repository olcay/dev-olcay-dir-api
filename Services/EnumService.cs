using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public static class EnumService
    {
        public static IEnumerable<EnumDto> GetPetTypes()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = (int)PetType.Cat, Name = "Kedi" },
                new EnumDto{ Id = (int)PetType.Dog, Name = "Köpek" }
            };
        }
    }
}