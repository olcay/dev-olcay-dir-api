using AutoMapper;

namespace WebApi.Profiles
{
    public class ImagesProfile : Profile
    {

        public ImagesProfile()
        {
            CreateMap<Entities.Image, Models.ImageDto>()
                .ForMember(destination => destination.Url, opts => opts.MapFrom<ImageUrlResolver>())
                .ForMember(destination => destination.ThumbnailUrl, opts => opts.MapFrom<ImageThumbnailUrlResolver>());
        }
    }
}
