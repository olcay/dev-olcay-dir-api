using AutoMapper;

namespace WebApi.Profiles
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<Entities.Tag, Models.TagDto>();
            CreateMap<Models.TagForCreationDto, Entities.Tag>();
            CreateMap<Models.TagForUpdateDto, Entities.Tag>();
            CreateMap<Entities.Tag, Models.TagForUpdateDto>();
        }
    }
}
