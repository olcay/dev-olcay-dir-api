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
            var ages = EnumService.GetAges();
            var genders = EnumService.GetGenders();
            var sizes = EnumService.GetSizes();
            var fromWhere = EnumService.GetFromWhere();
            var cities = EnumService.GetCities();

            CreateMap<Entities.Pet, Models.PetDto>()
            .ForMember(destination => destination.PetType,
               opts => opts.MapFrom(source => petTypes.FirstOrDefault(e => e.Id == (int)source.PetType).Name))
            .ForMember(destination => destination.Age,
               opts => opts.MapFrom(source => ages.FirstOrDefault(e => e.Id == (int)source.Age).Name))
            .ForMember(destination => destination.Gender,
               opts => opts.MapFrom(source => genders.FirstOrDefault(e => e.Id == (int)source.Gender).Name))
            .ForMember(destination => destination.City,
               opts => opts.MapFrom(source => cities.FirstOrDefault(e => e.Id == (int)source.CityId).Name))
            .ForMember(destination => destination.Size,
               opts => opts.MapFrom(source => sizes.FirstOrDefault(e => e.Id == (int)source.Size).Name))
            .ForMember(destination => destination.FromWhere,
               opts => opts.MapFrom(source => fromWhere.FirstOrDefault(e => e.Id == (int)source.FromWhere).Name))
            .ForMember(destination => destination.Race,
               opts => opts.MapFrom(source => source.Race.Name))
            .ForMember(destination => destination.CreatedBy,
               opts => opts.MapFrom(source => source.CreatedBy.DisplayName));
        }
    }
}
