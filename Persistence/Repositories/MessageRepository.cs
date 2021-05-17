using WebApi.Entities;
using WebApi.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WebApi.Persistence.Repositories
{
    public interface IMessageRepository
    {
        void Add(MessageBox messageBox);
        void Add(Message message);
        void Delete(Message message);
        Message Get(Guid messageId);
        MessageBox GetBox(Guid petId, int createdById);
        IEnumerable<MessageBox> GetBoxes(int createdById);
        void Update(MessageBox messageBox);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Message Get(Guid messageId)
        {
            if (messageId == Guid.Empty)
            {
                throw new AppException(nameof(messageId));
            }

            return _context.Messages
                    .SingleOrDefault(a => a.Id == messageId && a.IsDeleted == false);
        }

        public MessageBox GetBox(Guid messageBoxId)
        {
            if (messageBoxId == Guid.Empty)
            {
                throw new AppException(nameof(messageBoxId));
            }

            return _context.MessageBoxes
                    .SingleOrDefault(a => a.Id == messageBoxId && a.IsDeleted == false);
        }

        public MessageBox GetBox(Guid petId, int createdById)
        {
            if (petId == Guid.Empty)
            {
                throw new AppException(nameof(petId));
            }

            if (createdById == 0)
            {
                throw new AppException(nameof(createdById));
            }

            return _context.MessageBoxes
                    .SingleOrDefault(a => a.PetId == petId && a.CreatedById == createdById);
        }

        public IEnumerable<MessageBox> GetBoxes(int createdById)
        {
            if (createdById == 0)
            {
                throw new AppException(nameof(createdById));
            }

            return _context.MessageBoxes
                    .Where(a =>
                    (a.CreatedById == createdById || a.Pet.CreatedById == createdById)
                    && a.IsDeleted == false)
                    .OrderByDescending(m => m.Updated)
                    .ToList();
        }

        public IEnumerable<Message> GetMessages(Guid messageBoxId)
        {
            if (messageBoxId == null)
            {
                throw new AppException(nameof(messageBoxId));
            }

            return _context.Messages
                    .Where(a => a.MessageBoxId == messageBoxId
                    && a.IsDeleted == false)
                    .OrderByDescending(m => m.Created)
                    .ToList();
        }

        public int CountUnreadMessages(int accountId)
        {
            if (accountId == 0)
            {
                throw new AppException(nameof(accountId));
            }

            return _context.MessageBoxes
                    .Count(a =>
                    (a.CreatedById == accountId || a.Pet.CreatedById == accountId)
                    && a.IsDeleted == false 
                    && a.Messages.Any(m => !m.Read.HasValue));
        }

        public void Add(MessageBox messageBox)
        {
            if (messageBox == null)
            {
                throw new AppException(nameof(messageBox));
            }

            _context.MessageBoxes.Add(messageBox);
        }

        public void Update(MessageBox messageBox)
        {
            if (messageBox == null)
            {
                throw new AppException(nameof(messageBox));
            }

            _context.MessageBoxes.Update(messageBox);
        }

        public void Update(Message message)
        {
            if (message == null)
            {
                throw new AppException(nameof(message));
            }

            _context.Messages.Update(message);
        }

        public void Add(Message message)
        {
            if (message == null)
            {
                throw new AppException(nameof(message));
            }

            _context.Messages.Add(message);
        }

        public void Delete(Message message)
        {
            if (message == null)
            {
                throw new AppException(nameof(message));
            }

            _context.Messages.Remove(message);
        }
    }
}
