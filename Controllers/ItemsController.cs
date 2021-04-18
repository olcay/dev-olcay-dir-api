using AutoMapper;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.ResourceParameters;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    [Produces("application/json")]
    public class ItemsController : BaseController
    {
        private readonly IRepository _itemDirectoryRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public ItemsController(IRepository itemDirectoryRepository,
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _itemDirectoryRepository = itemDirectoryRepository ??
                throw new ArgumentNullException(nameof(itemDirectoryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }
        
        [HttpGet(Name = "GetItems")]
        [HttpHead]
        public ActionResult<LinkedCollectionResourceDto> GetItems(
            [FromQuery] ItemsResourceParameters itemsResourceParameters)
        {            
            if (!_propertyMappingService.ValidMappingExistsFor<ItemDto, Item>(itemsResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<ItemDto>(itemsResourceParameters.Fields))
            {
                return BadRequest();
            }

            var itemsFromRepo = _itemDirectoryRepository.GetItems(itemsResourceParameters);
                    
            var paginationMetadata = new PaginationDto
            (
                itemsFromRepo.TotalCount,
                itemsFromRepo.PageSize,
                itemsFromRepo.CurrentPage,
                itemsFromRepo.TotalPages 
            );

            var links = CreateLinksForItems(itemsResourceParameters,
                itemsFromRepo.HasNext,
                itemsFromRepo.HasPrevious);

            var shapedItems = _mapper.Map<IEnumerable<ItemDto>>(itemsFromRepo)
                               .ShapeData(itemsResourceParameters.Fields);

            var shapedItemsWithLinks = shapedItems.Select(item =>
            {
                var itemAsDictionary = item as IDictionary<string, object>;
                var itemLinks = CreateLinksForItem((Guid)itemAsDictionary["Id"], null);
                itemAsDictionary.Add("links", itemLinks);
                return itemAsDictionary;
            });

            var linkedCollectionResource = new LinkedCollectionResourceDto(shapedItemsWithLinks, links, paginationMetadata);

            return Ok(linkedCollectionResource);             
        }

        [Produces("application/json", 
            "application/vnd.marvin.hateoas+json",
            "application/vnd.marvin.item.full+json", 
            "application/vnd.marvin.item.full.hateoas+json",
            "application/vnd.marvin.item.friendly+json", 
            "application/vnd.marvin.item.friendly.hateoas+json")]
        [HttpGet("{itemId}", Name ="GetItem")]
        public ActionResult<IDictionary<string, object>> GetItem(Guid itemId, string fields,
              [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<ItemDto>
               (fields))
            {
                return BadRequest();
            }

            var itemFromRepo = _itemDirectoryRepository.GetItem(itemId);

            if (itemFromRepo == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix
               .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            IEnumerable<LinkDto> links = new List<LinkDto>();

            if (includeLinks)
            {
                links = CreateLinksForItem(itemId, fields);
            }

            var primaryMediaType = includeLinks ?
                parsedMediaType.SubTypeWithoutSuffix
                .Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8)
                : parsedMediaType.SubTypeWithoutSuffix;

            // full item
            if (primaryMediaType == "vnd.marvin.item.full")
            {
                var fullResourceToReturn = _mapper.Map<ItemFullDto>(itemFromRepo)
                    .ShapeData(fields) as IDictionary<string, object>;

                if (includeLinks)
                {
                    fullResourceToReturn.Add("links", links);
                }

                return Ok(fullResourceToReturn);
            }

            // friendly item
            var friendlyResourceToReturn = _mapper.Map<ItemDto>(itemFromRepo)
                .ShapeData(fields) as IDictionary<string, object>;

            if (includeLinks)
            {
                friendlyResourceToReturn.Add("links", links);
            }

            return Ok(friendlyResourceToReturn);
        }

        [HttpPost(Name = "CreateItem")]
        [Consumes(
            "application/json",
            "application/vnd.marvin.itemforcreation+json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult<ItemDto> CreateItem(ItemForCreationDto item)
        {
            if (!TryValidateModel(item))
            {
                return ValidationProblem(ModelState);
            }

            var itemEntity = _mapper.Map<Item>(item);
            itemEntity.CreatedById = Account.Id;

            _itemDirectoryRepository.AddItem(itemEntity);

            foreach (var tag in item.Tags)
            {
                var tagFromDb = _itemDirectoryRepository.GetTag(tag.Name);

                _itemDirectoryRepository.AddItemTag(itemEntity.Id, tagFromDb.Id);
            }

            _itemDirectoryRepository.Save();

            var itemToReturn = _mapper.Map<ItemDto>(itemEntity);

            var links = CreateLinksForItem(itemToReturn.Id, null);
             
            var linkedResourceToReturn = itemToReturn.ShapeData(null)
                as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetItem",
                new { itemId = linkedResourceToReturn["Id"] },
                linkedResourceToReturn);
        } 

        [HttpOptions]
        public IActionResult GetItemsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpDelete("{itemId}", Name = "DeleteItem")]
        public ActionResult DeleteItem(Guid itemId)
        {
            var itemFromRepo = _itemDirectoryRepository.GetItem(itemId);

            if (itemFromRepo == null)
            {
                return NotFound();
            }

            _itemDirectoryRepository.DeleteItem(itemFromRepo);

            _itemDirectoryRepository.Save();

            return NoContent();
        }

        [HttpPatch("{itemId}")]
        public ActionResult PartiallyUpdateTagForItem(Guid itemId,
            JsonPatchDocument<ItemForUpdateDto> patchDocument)
        {
            var itemFromRepo = _itemDirectoryRepository.GetItem(itemId);

            if (itemFromRepo == null)
            {
                return NotFound();
            }
            
            var itemToPatch = _mapper.Map<ItemForUpdateDto>(itemFromRepo);
            // add validation
            patchDocument.ApplyTo(itemToPatch, ModelState);

            if (!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(itemToPatch, itemFromRepo);

            _itemDirectoryRepository.UpdateItem(itemFromRepo);

            _itemDirectoryRepository.Save();

            return NoContent();
        }

        private string CreateItemsResourceUri(
           ItemsResourceParameters itemsResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetItems",
                      new
                      {
                          fields = itemsResourceParameters.Fields,
                          orderBy = itemsResourceParameters.OrderBy,
                          pageNumber = itemsResourceParameters.PageNumber - 1,
                          pageSize = itemsResourceParameters.PageSize,
                          searchQuery = itemsResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetItems",
                      new
                      {
                          fields = itemsResourceParameters.Fields,
                          orderBy = itemsResourceParameters.OrderBy,
                          pageNumber = itemsResourceParameters.PageNumber + 1,
                          pageSize = itemsResourceParameters.PageSize,
                          searchQuery = itemsResourceParameters.SearchQuery
                      });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetItems",
                    new
                    {
                        fields = itemsResourceParameters.Fields,
                        orderBy = itemsResourceParameters.OrderBy,
                        pageNumber = itemsResourceParameters.PageNumber,
                        pageSize = itemsResourceParameters.PageSize,
                        searchQuery = itemsResourceParameters.SearchQuery
                    });
            }

        }

        private IEnumerable<LinkDto> CreateLinksForItem(Guid itemId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetItem", new { itemId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetItem", new { itemId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
               new LinkDto(Url.Link("DeleteItem", new { itemId }),
               "delete_item",
               "DELETE"));

            links.Add(
                new LinkDto(Url.Link("CreateTagForItem", new { itemId }),
                "create_tag_for_item",
                "POST"));

            links.Add(
               new LinkDto(Url.Link("GetTagsForItem", new { itemId }),
               "tags",
               "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForItems(
            ItemsResourceParameters itemsResourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateItemsResourceUri(
                   itemsResourceParameters, ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateItemsResourceUri(
                      itemsResourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateItemsResourceUri(
                        itemsResourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

    }
}
