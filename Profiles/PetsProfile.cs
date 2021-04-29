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
            var statuses = EnumService.GetPetStatuses();

            CreateMap<Entities.Pet, Models.PetDto>()
            .ForMember(destination => destination.PetStatusText,
               opts => opts.MapFrom(source => petTypes.FirstOrDefault(e => e.Value == source.PetStatus.ToString()).Text))
            .ForMember(destination => destination.PetTypeText,
               opts => opts.MapFrom(source => petTypes.FirstOrDefault(e => e.Value == source.PetType.ToString()).Text))
            .ForMember(destination => destination.AgeText,
               opts => opts.MapFrom(source => ages.FirstOrDefault(e => e.Value == source.Age.ToString()).Text))
            .ForMember(destination => destination.GenderText,
               opts => opts.MapFrom(source => genders.FirstOrDefault(e => e.Value == source.Gender.ToString()).Text))
            .ForMember(destination => destination.CityText,
               opts => opts.MapFrom(source => cities.FirstOrDefault(e => e.Value == source.CityId.ToString()).Text))
            .ForMember(destination => destination.SizeText,
               opts => opts.MapFrom(source => sizes.FirstOrDefault(e => e.Value == source.Size.ToString()).Text))
            .ForMember(destination => destination.FromWhereText,
               opts => opts.MapFrom(source => fromWhere.FirstOrDefault(e => e.Value == source.FromWhere.ToString()).Text))
            .ForMember(destination => destination.RaceName,
               opts => opts.MapFrom(source => source.Race.Name));

            CreateMap<Entities.Pet, Models.PetFullDto>()
            .ForMember(destination => destination.PetStatusText,
               opts => opts.MapFrom(source => petTypes.FirstOrDefault(e => e.Value == source.PetStatus.ToString()).Text))
            .ForMember(destination => destination.PetType,
               opts => opts.MapFrom(source => petTypes.FirstOrDefault(e => e.Value == source.PetType.ToString())))
            .ForMember(destination => destination.Age,
               opts => opts.MapFrom(source => ages.FirstOrDefault(e => e.Value == source.Age.ToString())))
            .ForMember(destination => destination.Gender,
               opts => opts.MapFrom(source => genders.FirstOrDefault(e => e.Value == source.Gender.ToString())))
            .ForMember(destination => destination.City,
               opts => opts.MapFrom(source => cities.FirstOrDefault(e => e.Value == source.CityId.ToString())))
            .ForMember(destination => destination.Size,
               opts => opts.MapFrom(source => sizes.FirstOrDefault(e => e.Value == source.Size.ToString())))
            .ForMember(destination => destination.FromWhere,
               opts => opts.MapFrom(source => fromWhere.FirstOrDefault(e => e.Value == source.FromWhere.ToString())));

            CreateMap<Models.PetForCreationDto, Entities.Pet>();
            CreateMap<Models.PetForUpdateDto, Entities.Pet>().ReverseMap();
        }
    }
}
