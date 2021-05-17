using WebApi.Entities;
using System;

namespace WebApi.Persistence.Repositories
{
    public interface IImageRepository
    {
        void Add(Image image);
        void Delete(Image image);
        Image Get(Guid imageId);
    }
}
