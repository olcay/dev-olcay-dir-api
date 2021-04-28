using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;
using System.Collections.Generic;

namespace WebApi.Services
{
    public interface IRepository
    {    
        IEnumerable<Race> GetRaces(int petType);

        IEnumerable<Tag> GetTags(Guid itemId);
        ItemTag GetItemTag(Guid itemId, Guid tagId);
        Tag GetTag(string name);
        void AddItemTag(Guid itemId, Guid tagId);
        void DeleteItemTag(ItemTag itemTag);

        PagedList<Item> GetItems(ItemsResourceParameters itemsResourceParameters);
        Item GetItem(Guid itemId);
        IEnumerable<Item> GetItems(IEnumerable<Guid> itemIds);
        void AddItem(Item item);
        void DeleteItem(Item item);
        void UpdateItem(Item item);
        bool ItemExists(Guid itemId);
        bool Save();

        PagedList<Pet> GetPets(PetsResourceParameters resourceParameters);
        Pet GetPet(Guid petId);
        void AddPet(Pet pet);
        void UpdatePet(Pet pet);
    }
}
