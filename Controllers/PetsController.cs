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
using WebApi.Enums;
using WebApi.Persistence;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/pets")]
    [Produces("application/json")]
    public class PetsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public PetsController(
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet(Name = "GetPets")]
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

            var itemsFromRepo = _unitOfWork.Pets.Get(resourceParameters, Account);

            var paginationMetadata = new PaginationDto
            (
                itemsFromRepo.TotalCount,
                itemsFromRepo.PageSize,
                itemsFromRepo.CurrentPage,
                itemsFromRepo.TotalPages
            );

            var shapedItems = _mapper.Map<IEnumerable<PetDto>>(itemsFromRepo)
                                    .ShapeData(resourceParameters.Fields);

            var collectionResource = new CollectionResourceDto(shapedItems, paginationMetadata);

            return Ok(collectionResource);
        }

        [HttpGet("{petId}", Name = "GetPet")]
        public ActionResult<PetFullDto> GetPet(Guid petId)
        {
            var pet = _unitOfWork.Pets.Get(petId);

            if (pet.PetStatus != PetStatus.Published && !(pet.CreatedById == Account.Id || Account.Role == Role.Admin))
                return Unauthorized(new { message = "Unauthorized" });

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
            petEntity.Create(Account.Id);

            _unitOfWork.Pets.Add(petEntity);
            _unitOfWork.Complete(Account.Id);

            var petEntityFromRepo = _unitOfWork.Pets.Get(petEntity.Id);

            var petToReturn = _mapper.Map<PetFullDto>(petEntityFromRepo);

            var resourceToReturn = petToReturn.ShapeData(null);

            return CreatedAtRoute("GetPet", new { petId = petEntity.Id }, resourceToReturn);
        }

        [HttpOptions(Name = "GetPetsOptions")]
        public IActionResult GetPetsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpPut("{petId}", Name = "UpdatePet")]
        [Authorize]
        public IActionResult UpdatePet(Guid petId, PetForUpdateDto pet)
        {
            var petFromRepo = _unitOfWork.Pets.Get(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _mapper.Map(pet, petFromRepo);
            _unitOfWork.Pets.Update(petFromRepo);
            _unitOfWork.Complete(Account.Id);

            return NoContent();
        }

        [HttpPatch("{petId}", Name = "PartiallyUpdatePet")]
        [Authorize]
        public ActionResult PartiallyUpdatePet(Guid petId,
            JsonPatchDocument<PetForUpdateDto> patchDocument)
        {
            var petFromRepo = _unitOfWork.Pets.Get(petId);

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

            _unitOfWork.Pets.Update(petFromRepo);

            _unitOfWork.Complete(Account.Id);

            return NoContent();
        }

        [HttpPatch("{petId}/publish", Name = "PublishPet")]
        [Authorize]
        public IActionResult PublishPet(Guid petId)
        {
            var petFromRepo = _unitOfWork.Pets.Get(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            petFromRepo.Publish();

            _unitOfWork.Pets.Update(petFromRepo);
            _unitOfWork.Complete(Account.Id);

            return NoContent();
        }

        [HttpPatch("{petId}/unpublish", Name = "UnpublishPet")]
        [Authorize]
        public IActionResult UnpublishPet(Guid petId)
        {
            var petFromRepo = _unitOfWork.Pets.Get(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            petFromRepo.Unpublish();

            _unitOfWork.Pets.Update(petFromRepo);
            _unitOfWork.Complete(Account.Id);

            return NoContent();
        }

        [HttpDelete("{petId}", Name = "DeletePet")]
        [Authorize]
        public IActionResult DeletePet(Guid petId)
        {
            var petFromRepo = _unitOfWork.Pets.Get(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            petFromRepo.Delete();

            _unitOfWork.Pets.Update(petFromRepo);
            _unitOfWork.Complete(Account.Id);

            return NoContent();
        }
    }
}
