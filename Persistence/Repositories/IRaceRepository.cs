using WebApi.Entities;
using System.Collections.Generic;
using WebApi.Enums;

namespace WebApi.Persistence.Repositories
{
    public interface IRaceRepository
    {
        IEnumerable<Race> Get(PetType petType);
        bool Exists(int raceId);
    }
}
