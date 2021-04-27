using System.Linq;
using AutoMapper;
using WebApi.Services;

namespace WebApi.Profiles
{
    public class PetsProfile : Profile
    {
        public PetsProfile()
        {
            var petTypes = EnumService.GetPetTypes();

            CreateMap<Entities.Pet, Models.PetDto>()
            .ForMember(destination => destination.PetType,
               opts => opts.MapFrom(source => petTypes.FirstOrDefault(e => e.Id == (int)source.PetType).Name));
        }
    }
}
