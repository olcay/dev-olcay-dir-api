using AutoMapper;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.ResourceParameters;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public ActionResult<CollectionResourceDto> GetPets(
            [FromQuery] PetsResourceParameters resourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<PetDto, Pet>(resourceParameters.OrderBy))
            {
                throw new AppException(nameof(resourceParameters.OrderBy));
            }

            if (!_propertyCheckerService.TypeHasProperties<PetDto>(resourceParameters.Fields))
            {
                throw new AppException(nameof(resourceParameters.Fields));
            }

            var itemsFromRepo = _repository.GetPets(resourceParameters);

            var paginationMetadata = new PaginationDto
            (
                itemsFromRepo.TotalCount,
                itemsFromRepo.PageSize,
                itemsFromRepo.CurrentPage,
                itemsFromRepo.TotalPages
            );

            var shapedItems = _mapper.Map<IEnumerable<PetDto>>(itemsFromRepo)
                                    .ShapeData(resourceParameters.Fields);

            var linkedCollectionResource = new CollectionResourceDto(shapedItems, paginationMetadata);

            return Ok(linkedCollectionResource);
        }

        [HttpGet("{petId}", Name = "GetPet")]
        public ActionResult<PetFullDto> GetPet(Guid petId)
        {
            var pet = _repository.GetPet(petId);

            var petDto = _mapper.Map<PetFullDto>(pet);

            return Ok(petDto);
        }

        [HttpPost(Name = "CreatePet")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult<PetFullDto> CreatePet(PetForCreationDto pet)
        {
            var petEntity = _mapper.Map<Pet>(pet);
            petEntity.CreatedById = Account.Id;
            petEntity.Created = DateTimeOffset.UtcNow;

            _repository.AddPet(petEntity);
            _repository.Save();

            var petEntityFromRepo = _repository.GetPet(petEntity.Id);

            var petToReturn = _mapper.Map<PetFullDto>(petEntityFromRepo);

            var resourceToReturn = petToReturn.ShapeData(null);

            return CreatedAtRoute("GetPet", new { petId = petEntity.Id }, resourceToReturn);
        }

        [HttpOptions]
        public IActionResult GetPetsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpPut("{petId}")]
        [Authorize]
        public IActionResult UpdatePet(Guid petId, PetForUpdateDto pet)
        {
            var petFromRepo = _repository.GetPet(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _mapper.Map(pet, petFromRepo);
            _repository.UpdatePet(petFromRepo);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{petId}")]
        [Authorize]
        public ActionResult PartiallyUpdatePet(Guid petId,
            JsonPatchDocument<PetForUpdateDto> patchDocument)
        {
            var petFromRepo = _repository.GetPet(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });
            
            var itemToPatch = _mapper.Map<PetForUpdateDto>(petFromRepo);
            // add validation
            patchDocument.ApplyTo(itemToPatch, ModelState);

            if (!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(itemToPatch, petFromRepo);

            _repository.UpdatePet(petFromRepo);

            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{petId}/publish")]
        [Authorize]
        public IActionResult PublishPet(Guid petId)
        {
            var petFromRepo = _repository.GetPet(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            petFromRepo.Published = DateTimeOffset.UtcNow;

            _repository.UpdatePet(petFromRepo);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{petId}")]
        [Authorize]
        public IActionResult DeletePet(Guid petId)
        {
            var petFromRepo = _repository.GetPet(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            petFromRepo.Deleted = DateTimeOffset.UtcNow;

            _repository.UpdatePet(petFromRepo);
            _repository.Save();

            return NoContent();
        }
    }
}
