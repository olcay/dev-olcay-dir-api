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
    [Authorize]
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

        // POST /api/pets/{petId}/messageBoxes
        [HttpPost("pets/{petId}/messageBoxes", Name = "GenerateMessageBox")]
        public ActionResult<MessageBoxDto> GenerateMessageBox(Guid petId)
        {
            var messageBox = _unitOfWork.Messages.GetBox(petId, Account.Id);

            if (messageBox == null)
            {
                var pet = _unitOfWork.Pets.Get(petId);

                if (pet.CreatedById == Account.Id)
                    throw new AppException("User cannot send message to self");

                messageBox = new MessageBox(Account.Id, pet.Id, pet.CreatedById);

                _unitOfWork.Messages.Add(messageBox);
                _unitOfWork.Complete();
            }
            
            return Ok(_mapper.Map<MessageBoxDto>(messageBox));
        }

        // PUT /api/pets/{petId}/messageBoxes/{messageBoxId}
        [HttpPut("pets/{petId}/messageBoxes/{messageBoxId}", Name = "ReadMessageBox")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ReadMessageBox(Guid petId, Guid messageBoxId)
        {
            var messageBox = _unitOfWork.Messages.GetBox(petId, messageBoxId, Account.Id);

            if (messageBox == null)
                throw new KeyNotFoundException("Message box not found");

            messageBox.ReadBy(Account.Id);
            _unitOfWork.Messages.Update(messageBox);

            _unitOfWork.Complete();
            return new AcceptedResult();
        }

        // POST /api/pets/{petId}/messageBoxes/{messageBoxId}/messages
        [HttpPost("pets/{petId}/messageBoxes/{messageBoxId}/messages", Name = "SendMessage")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SendMessage(Guid petId, Guid messageBoxId, MessageForCreationDto messageDto)
        {
            var messageBox = _unitOfWork.Messages.GetBox(petId, messageBoxId, Account.Id);

            if (messageBox == null)
                throw new KeyNotFoundException("Message box not found");

            var message = new Message(messageDto.Body, Account.Id, messageBox);
            _unitOfWork.Messages.Add(message);

            _unitOfWork.Complete();
            return new AcceptedResult();
        }

        // GET /api/pets/messageBoxes
        [HttpGet("pets/messageBoxes", Name = "GetMessageBoxes")]
        public ActionResult<IEnumerable<MessageBoxDto>> GetMessageBoxes()
        {
            var boxes = _unitOfWork.Messages.GetBoxes(Account.Id);
            return Ok(_mapper.Map<IEnumerable<MessageBoxDto>>(boxes));
        }

        // GET /api/pets/{petId}/messageBoxes/{messageBoxId}/messages
        [HttpGet("pets/{petId}/messageBoxes/{messageBoxId}/messages", Name = "GetMessages")]
        public ActionResult<IEnumerable<MessageDto>> GetMessages(Guid petId, Guid messageBoxId)
        {
            var messageBox = _unitOfWork.Messages.GetBox(petId, messageBoxId, Account.Id);

            if (messageBox == null)
                throw new KeyNotFoundException("Message box not found");

            var messages = _unitOfWork.Messages.GetMessages(messageBox.Id);
            return Ok(_mapper.Map<IEnumerable<MessageDto>>(messages));
        }

        // GET /api/pets/messageBoxes/unreadMessageCount
        [HttpGet("pets/messageBoxes/unreadMessageCount", Name = "GetUnreadMessageCount")]
        public ActionResult<int> GetUnreadMessageCount()
        {
            var count = _unitOfWork.Messages.CountUnreadMessages(Account.Id);
            return Ok(count);
        }

        [HttpDelete("pets/{petId}/messageBoxes/{messageBoxId}/messages/{messageId}", Name = "DeleteMessage")]
        public IActionResult DeleteMessage(Guid petId, Guid messageId)
        {
            var message = _unitOfWork.Messages.Get(messageId);

            if (message.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            message.Delete();
            _unitOfWork.Messages.Update(message);

            return NoContent();
        }

        [HttpDelete("pets/{petId}/messageBoxes/{messageBoxId}", Name = "DeleteMessageBox")]
        public IActionResult DeleteMessageBox(Guid petId, Guid messageBoxId)
        {
            var messageBox = _unitOfWork.Messages.GetBox(messageBoxId);

            if (messageBox.CreatedById != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var participant = _unitOfWork.Messages.GetBoxParticipant(messageBoxId, Account.Id);

            participant.Delete();
            _unitOfWork.Messages.Update(participant);

            return NoContent();
        }
    }
}
