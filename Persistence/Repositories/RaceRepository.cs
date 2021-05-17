using WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Enums;

namespace WebApi.Persistence.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly DataContext _context;

        public RaceRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Race> Get(PetType petType)
        {
            return _context.Races
                .Where(c => c.PetType == petType)
                .OrderBy(c => c.Name).ToList();
        }

        public bool Exists(int raceId)
        {
            return _context.Races.Any(a => a.Id == raceId);
        }
    }
}
