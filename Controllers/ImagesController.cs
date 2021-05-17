using WebApi.Helpers;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi.Persistence;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/pets/{petId}/images")]
    [Produces("application/json")]
    public class ImagesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AzureStorageConfig storageConfig = null;

        public ImagesController(IUnitOfWork unitOfWork, IOptions<AzureStorageConfig> config)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            storageConfig = config.Value;
        }

        // POST /api/pets/{petId}/images
        [HttpPost(Name = "AddImage")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> AddImageAsync(Guid petId, ICollection<IFormFile> files)
        {
            var pet = _unitOfWork.Pets.Get(petId);

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
                                _unitOfWork.Images.Add(image);
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
                _unitOfWork.Complete(Account.Id);
                return new AcceptedResult();
            }
            else
                throw new AppException("Looks like the image could not upload to the storage");
        }

        [HttpDelete("{imageId}", Name = "DeleteImage")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(Guid petId, Guid imageId)
        {
            var petFromRepo = _unitOfWork.Pets.Get(petId);

            if (petFromRepo.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var image = _unitOfWork.Images.Get(imageId);

            var isDeleted = await StorageHelper.DeleteFileFromStorage(image.FileName(), storageConfig);

            if (isDeleted)
            {
                _unitOfWork.Images.Delete(image);
                _unitOfWork.Complete(Account.Id);
            }

            return NoContent();
        }
    }
}
