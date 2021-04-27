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
    [Route("api/pets")]
    [Produces("application/json")]
    public class PetsController : BaseController
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public PetsController(IRepository repository,
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet(Name = "GetPets")]
        [HttpHead]
        public ActionResult<LinkedCollectionResourceDto> GetPets(
            [FromQuery] PetsResourceParameters resourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<PetDto, Pet>(resourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<PetDto>(resourceParameters.Fields))
            {
                return BadRequest();
            }

            var itemsFromRepo = _repository.GetPets(resourceParameters);

            var paginationMetadata = new PaginationDto
            (
                itemsFromRepo.TotalCount,
                itemsFromRepo.PageSize,
                itemsFromRepo.CurrentPage,
                itemsFromRepo.TotalPages
            );

            var links = CreateLinksForPets(resourceParameters,
                itemsFromRepo.HasNext,
                itemsFromRepo.HasPrevious);

            var shapedItems = _mapper.Map<IEnumerable<PetDto>>(itemsFromRepo)
                               .ShapeData(resourceParameters.Fields);

            var shapedItemsWithLinks = shapedItems.Select(item =>
            {
                var itemAsDictionary = item as IDictionary<string, object>;
                var itemLinks = CreateLinksForPet((Guid)itemAsDictionary["Id"], null);
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
        [HttpGet("{petId}", Name = "GetPet")]
        public ActionResult<IDictionary<string, object>> GetPet(Guid petId, string fields,
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

            var itemFromRepo = _repository.GetItem(petId);

            if (itemFromRepo == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix
               .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            IEnumerable<LinkDto> links = new List<LinkDto>();

            if (includeLinks)
            {
                links = CreateLinksForPet(petId, fields);
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

        [HttpPost(Name = "CreatePet")]
        [Consumes(
            "application/json",
            "application/vnd.marvin.itemforcreation+json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult<ItemDto> CreatePet(ItemForCreationDto item)
        {
            if (!TryValidateModel(item))
            {
                return ValidationProblem(ModelState);
            }

            var itemEntity = _mapper.Map<Item>(item);
            itemEntity.CreatedById = Account.Id;

            _repository.AddItem(itemEntity);

            foreach (var tag in item.Tags)
            {
                var tagFromDb = _repository.GetTag(tag.Name);

                _repository.AddItemTag(itemEntity.Id, tagFromDb.Id);
            }

            _repository.Save();

            var itemToReturn = _mapper.Map<ItemDto>(itemEntity);

            var links = CreateLinksForPet(itemToReturn.Id, null);

            var linkedResourceToReturn = itemToReturn.ShapeData(null)
                as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetPet",
                new { petId = linkedResourceToReturn["Id"] },
                linkedResourceToReturn);
        }

        [HttpOptions]
        public IActionResult GetPetsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpDelete("{itemId}", Name = "DeletePet")]
        public ActionResult DeletePet(Guid itemId)
        {
            var itemFromRepo = _repository.GetItem(itemId);

            if (itemFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteItem(itemFromRepo);

            _repository.Save();

            return NoContent();
        }

        private string CreatePetsResourceUri(
           PetsResourceParameters resourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPets",
                      new
                      {
                          fields = resourceParameters.Fields,
                          orderBy = resourceParameters.OrderBy,
                          pageNumber = resourceParameters.PageNumber - 1,
                          pageSize = resourceParameters.PageSize,
                          searchQuery = resourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetPets",
                      new
                      {
                          fields = resourceParameters.Fields,
                          orderBy = resourceParameters.OrderBy,
                          pageNumber = resourceParameters.PageNumber + 1,
                          pageSize = resourceParameters.PageSize,
                          searchQuery = resourceParameters.SearchQuery
                      });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetPets",
                    new
                    {
                        fields = resourceParameters.Fields,
                        orderBy = resourceParameters.OrderBy,
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize,
                        searchQuery = resourceParameters.SearchQuery
                    });
            }

        }

        private IEnumerable<LinkDto> CreateLinksForPet(Guid petId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetPet", new { petId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetPet", new { petId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
               new LinkDto(Url.Link("DeletePet", new { petId }),
               "delete_pet",
               "DELETE"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForPets(
            PetsResourceParameters resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreatePetsResourceUri(
                   resourceParameters, ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreatePetsResourceUri(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreatePetsResourceUri(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

    }
}
