using System;
using System.Collections.Generic;
using ForumNew.BLL.DTO;
using ForumNew.BLL.Infrastructure;


namespace ForumNew.BLL.Interfaces
{
    public interface IMessageService : IDisposable
    {
        DTOMessageHeader MessageHeader(int id);

        IEnumerable<DTOMessageViewModel> GetAllMessages(int id, ref int pageNumber,int pageSize, out int totalPages);

        bool CreateMessage(DTOCreateMessageViewModel dtoCreateMessageViewModel);

        bool DeleteMessage(DTODeleteMessageViewModel dtoDeleteMessageViewModel);

        string GetMessage(DTOEditMessageViewModel dtoEditMessageViewModel);

        string EditMessageConfirm(DTOEditMessageViewModel dtoEditMessageViewModel);
    }
}
