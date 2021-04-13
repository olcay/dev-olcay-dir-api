using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Services
{
    public class ItemDirectoryRepository : IRepository, IDisposable
    {
        private readonly DataContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public ItemDirectoryRepository(DataContext context,
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

            _context.ItemTags.Add(new ItemTag(){ ItemId = itemId, TagId = tagId }); 
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
                        .Where(c => c.ItemTags.Any(i=> i.ItemId == itemId))
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
    }
}
