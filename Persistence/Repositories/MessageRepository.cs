using WebApi.Entities;
using WebApi.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Persistence.Repositories
{
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

        public MessageBoxParticipant GetBoxParticipant(Guid messageBoxId, int accountId)
        {
            if (messageBoxId == Guid.Empty)
            {
                throw new AppException(nameof(messageBoxId));
            }

            return _context.MessageBoxParticipants
                    .SingleOrDefault(a => a.MessageBoxId == messageBoxId && a.AccountId == accountId);
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

        public MessageBox GetBox(Guid petId, Guid messageBoxId, int accountId)
        {
            return _context.MessageBoxes
                    .Include(b => b.MessageBoxParticipants)
                    .SingleOrDefault(a => a.Id == messageBoxId 
                    && a.PetId == petId 
                    && a.IsDeleted == false 
                    && a.MessageBoxParticipants.Any(p => p.AccountId == accountId));
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

        public IEnumerable<MessageBoxParticipant> GetBoxes(int accountId)
        {
            if (accountId == 0)
            {
                throw new AppException(nameof(accountId));
            }

            return _context.MessageBoxParticipants
                    .Include(p => p.MessageBox)
                    .Include(p => p.MessageBox.Pet)
                    .Where(p => p.MessageBox.IsDeleted == false && p.AccountId == accountId)
                    .OrderByDescending(p => p.MessageBox.Updated)
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

            return _context.MessageBoxParticipants
                    .Include(a => a.MessageBox)
                    .Count(a =>
                    a.AccountId == accountId
                    && a.MessageBox.IsDeleted == false 
                    && a.Read < a.MessageBox.Updated);
        }

        public void Add(MessageBoxParticipant accountMessageBox)
        {
            if (accountMessageBox == null)
            {
                throw new AppException(nameof(accountMessageBox));
            }

            _context.MessageBoxParticipants.Add(accountMessageBox);
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
