using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public class Repository : IRepository, IDisposable
    {
        private readonly DataContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public Repository(DataContext context,
            IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void AddItemTag(Guid itemId, Guid tagId)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            if (tagId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(tagId));
            }

            _context.ItemTags.Add(new ItemTag() { ItemId = itemId, TagId = tagId });
        }

        public void DeleteItemTag(ItemTag itemTag)
        {
            if (itemTag != null) _context.ItemTags.Remove(itemTag);
        }

        public ItemTag GetItemTag(Guid itemId, Guid tagId)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            if (tagId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(tagId));
            }

            return _context.ItemTags.FirstOrDefault(c => c.TagId == tagId && c.ItemId == itemId);
        }

        public Tag GetTag(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = name.ToLowerInvariant();

            var tag = _context.Tags.FirstOrDefault(c => c.Name == name);

            if (tag != null) return tag;

            tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _context.Tags.Add(tag);

            return tag;
        }

        public IEnumerable<Tag> GetTags(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return _context.Tags
                        .Where(c => c.ItemTags.Any(i => i.ItemId == itemId))
                        .OrderBy(c => c.Name).ToList();
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // the repository fills the id (instead of using identity columns)
            item.Id = Guid.NewGuid();

            _context.Items.Add(item);
        }

        public bool ItemExists(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return _context.Items.Any(a => a.Id == itemId);
        }

        public void DeleteItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Items.Remove(item);
        }

        public Item GetItem(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return _context.Items.FirstOrDefault(a => a.Id == itemId);
        }

        public PagedList<Item> GetItems(ItemsResourceParameters itemsResourceParameters)
        {
            if (itemsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(itemsResourceParameters));
            }

            var collection = _context.Items as IQueryable<Item>;

            if (itemsResourceParameters.CreatedById > 0)
            {
                collection = collection.Where(a => a.CreatedById == itemsResourceParameters.CreatedById);
            }

            if (!string.IsNullOrWhiteSpace(itemsResourceParameters.SearchQuery))
            {
                var searchQuery = itemsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(itemsResourceParameters.OrderBy))
            {
                // get property mapping dictionary
                var itemPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<Models.ItemDto, Item>();

                collection = collection.ApplySort(itemsResourceParameters.OrderBy,
                    itemPropertyMappingDictionary);
            }

            return PagedList<Item>.Create(collection,
                itemsResourceParameters.PageNumber,
                itemsResourceParameters.PageSize);
        }

        public IEnumerable<Item> GetItems(IEnumerable<Guid> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            return _context.Items.Where(a => itemIds.Contains(a.Id))
                .OrderBy(a => a.Title)
                .ToList();
        }

        public void UpdateItem(Item item)
        {
            _context.Items.Update(item);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        public IEnumerable<Race> GetRaces(int petType)
        {
            if (!Enum.IsDefined(typeof(PetType), petType))
            {
                throw new AppException("Invalid pet type");
            }

            var petTypeEnum = (PetType)petType;

            return _context.Races
                .Where(c => c.PetType == petTypeEnum)
                .OrderBy(c => c.Name).ToList();
        }

        public PagedList<Pet> GetPets(PetsResourceParameters resourceParameters)
        {
            if (resourceParameters == null)
            {
                throw new ArgumentNullException(nameof(resourceParameters));
            }

            var collection = _context.Pets
                                .Include(c => c.Race)
                                .Where(c => !c.Deleted.HasValue);

            if (resourceParameters.PetTypeId > 0)
            {
                if (!Enum.IsDefined(typeof(PetType), resourceParameters.PetTypeId))
                {
                    throw new AppException("Invalid pet type");
                }

                var petTypeEnum = (PetType)resourceParameters.PetTypeId;

                collection = collection.Where(a => a.PetType == petTypeEnum);
            }

            if (resourceParameters.CreatedById > 0)
            {
                collection = collection.Where(a => a.CreatedById == resourceParameters.CreatedById);
            }

            if (resourceParameters.CityId > 0)
            {
                collection = collection.Where(a => a.CityId == resourceParameters.CityId);
            }

            if (resourceParameters.RaceId > 0)
            {
                collection = collection.Where(a => a.RaceId == resourceParameters.RaceId);
            }

            if (resourceParameters.AgeId > 0)
            {
                if (!Enum.IsDefined(typeof(PetAge), resourceParameters.AgeId))
                {
                    throw new AppException("Invalid age id");
                }

                var enumAge = (PetAge)resourceParameters.AgeId;

                collection = collection.Where(a => a.Age == enumAge);
            }

            if (resourceParameters.GenderId > 0)
            {
                if (!Enum.IsDefined(typeof(Gender), resourceParameters.GenderId))
                {
                    throw new AppException("Invalid gender id");
                }

                var enumGender = (Gender)resourceParameters.GenderId;

                collection = collection.Where(a => a.Gender == enumGender);
            }

            if (resourceParameters.SizeId > 0)
            {
                if (!Enum.IsDefined(typeof(Size), resourceParameters.SizeId))
                {
                    throw new AppException("Invalid size id");
                }

                var enumSize = (Size)resourceParameters.SizeId;

                collection = collection.Where(a => a.Size == enumSize);
            }

            if (resourceParameters.FromWhereId > 0)
            {
                if (!Enum.IsDefined(typeof(FromWhere), resourceParameters.FromWhereId))
                {
                    throw new AppException("Invalid from where id");
                }

                var enumFromWhere = (FromWhere)resourceParameters.FromWhereId;

                collection = collection.Where(a => a.FromWhere == enumFromWhere);
            }

            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(resourceParameters.OrderBy))
            {
                // get property mapping dictionary
                var propertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<Models.PetDto, Pet>();

                collection = collection.ApplySort(resourceParameters.OrderBy,
                    propertyMappingDictionary);
            }

            return PagedList<Pet>.Create(collection,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);
        }

        public Pet GetPet(Guid petId)
        {
            if (petId == Guid.Empty)
            {
                throw new AppException(nameof(petId));
            }

            var pet = _context.Pets.Include(p => p.Race).FirstOrDefault(a => a.Id == petId);

            if (pet == null)
            {
                throw new KeyNotFoundException("Pet not found");
            }

            return pet;
        }

        public void AddPet(Pet pet)
        {
            if (pet == null)
            {
                throw new AppException(nameof(pet));
            }

            // the repository fills the id (instead of using identity columns)
            pet.Id = Guid.NewGuid();

            _context.Pets.Add(pet);
        }

        public void UpdatePet(Pet pet)
        {
            _context.Pets.Update(pet);
        }
    }
}
