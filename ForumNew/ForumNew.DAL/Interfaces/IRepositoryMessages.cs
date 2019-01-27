using ForumNew.DAL.Entities;
using System;
using System.Collections.Generic;

namespace ForumNew.DAL.Interfaces
{
    public interface IRepositoryMessages
    {
        Theme MessageHeader(int id);

        IEnumerable<Message> GetAllMessages(int id, ref int pageNumber, int pageSize, out int totalPages);

        bool CreateMessage(Message message);

        bool DeleteMessage(Message message);

        string GetMessage(Message message);

        string EditMessageConfirm(Message message);
    }
}
