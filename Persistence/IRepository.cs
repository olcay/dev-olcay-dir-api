using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;
using System.Collections.Generic;
using WebApi.Enums;

namespace WebApi.Persistence.Services
{
    public interface IRepository
    {
        bool Save(int? accountId);

        IEnumerable<Race> GetRaces(PetType petType);

        bool RaceExists(int raceId);

        PagedList<Pet> GetPets(PetsResourceParameters resourceParameters, Account account);

        Pet GetPet(Guid petId);

        void AddPet(Pet pet);

        void UpdatePet(Pet pet);

        void AddImage(Image image);
        
        void DeleteImage(Image image);
        Image GetImage(Guid imageId);
    }
}
