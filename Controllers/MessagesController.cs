using AutoMapper;
using WebApi.Helpers;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebApi.Persistence;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class MessagesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AzureStorageConfig storageConfig = null;

        public MessagesController(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AzureStorageConfig> config)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            storageConfig = config.Value;
        }

        // POST /api/pets/{petId}/messages
        [HttpPost("pets/{petId}/messages", Name = "SendMessage")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public IActionResult SendMessage(Guid petId, MessageForCreationDto messageDto)
        {
            var pet = _unitOfWork.Pets.Get(petId);

            var messageBox = _unitOfWork.Messages.GetBox(petId, Account.Id);

            if (messageBox == null)
            {
                if (pet.CreatedById == Account.Id)
                    throw new AppException("User cannot send message to self");

                messageBox = new MessageBox(){
                    CreatedById = Account.Id,
                    Id = Guid.NewGuid(),
                    PetId = petId,
                    Updated = DateTimeOffset.UtcNow
                };

                _unitOfWork.Messages.Add(messageBox);
            } else {
                messageBox.Updated = DateTimeOffset.UtcNow;
                _unitOfWork.Messages.Update(messageBox);
            }

            var message = new Message(){
                Body = messageDto.Body,
                CreatedById = Account.Id,
                MessageBoxId = messageBox.Id,
                Id = Guid.NewGuid(),
                Created = DateTimeOffset.UtcNow
            };

            _unitOfWork.Messages.Add(message);
            
            _unitOfWork.Complete(Account.Id);
            return new AcceptedResult();
        }

        [HttpGet("messages", Name = "GetMessageBoxes")]
        [Authorize]
        public ActionResult<IEnumerable<MessageBoxDto>> GetMessageBoxes()
        {
            var boxes = _unitOfWork.Messages.GetBoxes(Account.Id);
            return Ok(_mapper.Map<IEnumerable<MessageBoxDto>>(boxes));
        }

        [HttpDelete("pets/{petId}/messages/{messageId}", Name = "DeleteMessage")]
        [Authorize]
        public IActionResult DeleteMessage(Guid petId, Guid messageId)
        {
            var message = _unitOfWork.Messages.Get(messageId);

            if (message.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _unitOfWork.Messages.Delete(message);

            return NoContent();
        }
    }
}
