using AutoMapper;

namespace WebApi.Profiles
{
    public class RacesProfile : Profile
    {
        public RacesProfile()
        {
            CreateMap<Entities.Race, Models.RaceDto>();
        }
    }
}
