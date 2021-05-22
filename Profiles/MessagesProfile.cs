using AutoMapper;

namespace WebApi.Profiles
{
    public class MessagesProfile : Profile
    {
        public MessagesProfile()
        {
            CreateMap<Entities.MessageBoxParticipant, Models.MessageBoxDto>()
            .ForMember(destination => destination.Id,
               opts => opts.MapFrom(source => source.MessageBox.Id))
            .ForMember(destination => destination.PetId,
               opts => opts.MapFrom(source => source.MessageBox.Pet.Id))
            .ForMember(destination => destination.PetTitle,
               opts => opts.MapFrom(source => source.MessageBox.Pet.Title))
            .ForMember(destination => destination.IsRead,
               opts => opts.MapFrom(source => source.Read < source.MessageBox.Updated));
            CreateMap<Entities.Message, Models.MessageDto>();
        }
    }
}
