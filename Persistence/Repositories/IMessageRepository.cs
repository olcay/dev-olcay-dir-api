using WebApi.Entities;
using System;
using System.Collections.Generic;

namespace WebApi.Persistence.Repositories
{
    public interface IMessageRepository
    {
        void Add(MessageBox messageBox);
        void Add(Message message);
        void Add(MessageBoxParticipant accountMessageBox);
        int CountUnreadMessages(int accountId);
        Message Get(Guid messageId);
        MessageBox GetBox(Guid petId, int createdById);
        MessageBox GetBox(Guid messageBoxId);
        MessageBox GetBox(Guid petId, Guid messageBoxId, int accountId);
        IEnumerable<MessageBoxParticipant> GetBoxes(int accountId);
        MessageBoxParticipant GetBoxParticipant(Guid messageBoxId, int accountId);
        IEnumerable<Message> GetMessages(Guid messageBoxId);
        void Update(MessageBox messageBox);
        void Update(Message message);
        void Update(MessageBoxParticipant participant);
    }
}
