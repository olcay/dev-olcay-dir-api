using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;

namespace WebApi.Persistence.Repositories
{
    public interface IPetRepository
    {
        PagedList<Pet> Get(PetsResourceParameters resourceParameters, Account account);

        Pet Get(Guid petId);

        void Add(Pet pet);

        void Update(Pet pet);
    }
}
