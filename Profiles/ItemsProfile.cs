using AutoMapper;

namespace WebApi.Profiles
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<Entities.Item, Models.ItemDto>();
            CreateMap<Models.ItemForCreationDto, Entities.Item>();
            CreateMap<Models.ItemForUpdateDto, Entities.Item>().ReverseMap();
            CreateMap<Entities.Item, Models.ItemFullDto>();
        }
    }
}
