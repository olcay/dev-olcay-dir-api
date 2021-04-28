using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Models
{
    public class PetForCreationDto : PetForManipulationDto
    {
        public PetType PetType { get; set; }

    }
}
