using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;
using System.Collections.Generic;

namespace WebApi.Services
{
    public interface IRepository
    {
        bool Save(int? accountId);

        IEnumerable<Race> GetRaces(int petType);

        bool RaceExists(int raceId);

        PagedList<Pet> GetPets(PetsResourceParameters resourceParameters);

        Pet GetPet(Guid petId);

        void AddPet(Pet pet);

        void UpdatePet(Pet pet);
    }
}
