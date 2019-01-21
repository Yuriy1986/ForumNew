using System;
using System.Collections.Generic;
using ForumNew.BLL.DTO;
using ForumNew.BLL.Infrastructure;


namespace ForumNew.BLL.Interfaces
{
    public interface IMessageService : IDisposable
    {
        DTOMessageHeader MessageHeader(int id);

        IEnumerable<DTOMessageViewModel> GetAllMessages(int id);

        void CreateMessage(DTOCreateMessageViewModel dtoCreateMessageViewModel);

        bool DeleteMessage(DTODeleteMessageViewModel dtoDeleteMessageViewModel);

        string GetMessage(DTOEditMessageViewModel dtoEditMessageViewModel);

        string EditMessageConfirm(DTOEditMessageViewModel dtoEditMessageViewModel);
    }
}
