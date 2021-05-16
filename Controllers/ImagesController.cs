using AutoMapper;
using WebApi.Helpers;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;
using WebApi.Persistence.Services;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi.Enums;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/pets/{petId}/images")]
    [Produces("application/json")]
    public class ImagesController : BaseController
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly AzureStorageConfig storageConfig = null;

        public ImagesController(IRepository repository, IMapper mapper, IOptions<AzureStorageConfig> config)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            storageConfig = config.Value;
        }

        // POST /api/pets/{petId}/images
        [HttpPost(Name = "AddImage")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> AddImageAsync(Guid petId, ICollection<IFormFile> files)
        {
            var pet = _repository.GetPet(petId);

            if (pet.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            bool isUploaded = false;

            if (files.Count == 0)
                throw new AppException("No files received from the upload");

            if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)
                throw new AppException("Sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

            if (storageConfig.ImageContainer == string.Empty)
                throw new AppException("Please provide a name for your image container in the azure blob storage");

            foreach (var formFile in files)
            {
                if (StorageHelper.IsImage(formFile))
                {
                    if (formFile.Length > 0)
                    {
                        using (Stream stream = formFile.OpenReadStream())
                        {
                            var image = new Image();
                            image.Create(pet.Id, formFile.FileName);

                            isUploaded = await StorageHelper.UploadFileToStorage(stream, image.FileName(), storageConfig);

                            if (isUploaded)
                            {
                                _repository.AddImage(image);
                            }
                        }
                    }
                }
                else
                {
                    return new UnsupportedMediaTypeResult();
                }
            }

            if (isUploaded)
            {
                _repository.Save(Account.Id);
                return new AcceptedResult();
            }
            else
                throw new AppException("Looks like the image could not upload to the storage");
        }

        [HttpDelete("{imageId}", Name = "DeleteImage")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(Guid petId, Guid imageId)
        {
            var petFromRepo = _repository.GetPet(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var image = _repository.GetImage(imageId);

            var isDeleted = await StorageHelper.DeleteFileFromStorage(image.FileName(), storageConfig);

            if (isDeleted)
            {
                _repository.DeleteImage(image);
                _repository.Save(Account.Id);
            }

            return NoContent();
        }
    }
}
