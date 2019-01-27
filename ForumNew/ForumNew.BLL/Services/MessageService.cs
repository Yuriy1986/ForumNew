using ForumNew.BLL.DTO;
using ForumNew.DAL.Entities;
using ForumNew.BLL.Interfaces;
using ForumNew.BLL.Infrastructure;
using ForumNew.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace ForumNew.BLL.Services
{
    public class MessageService:IMessageService
    {
        IUnitOfWork Database { get; set; }

        public MessageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public DTOMessageHeader MessageHeader(int id)
        {
            return Mapper.Map<DTOMessageHeader>(Database.Messages.MessageHeader(id));
        }

        public IEnumerable<DTOMessageViewModel> GetAllMessages(int id, ref int pageNumber, int pageSize, out int totalPages)
        {
            return Mapper.Map<IEnumerable<Message>, IEnumerable<DTOMessageViewModel>>
                (Database.Messages.GetAllMessages(id, ref pageNumber, pageSize, out totalPages));
        }

        public bool CreateMessage(DTOCreateMessageViewModel dtoCreateMessageViewModel)
        {
            var message = Mapper.Map<Message>(dtoCreateMessageViewModel);
            return Database.Messages.CreateMessage(message);
        }

        public bool DeleteMessage(DTODeleteMessageViewModel dtoDeleteMessageViewModel)
        {
            var message = Mapper.Map<Message>(dtoDeleteMessageViewModel);
            return Database.Messages.DeleteMessage(message);
        }

        public string GetMessage(DTOEditMessageViewModel dtoEditMessageViewModel)
        {
            var message = Mapper.Map<Message>(dtoEditMessageViewModel);
            return Database.Messages.GetMessage(message);
        }

        public string EditMessageConfirm(DTOEditMessageViewModel dtoEditMessageViewModel)
        {
            var message = Mapper.Map<Message>(dtoEditMessageViewModel);
            return Database.Messages.EditMessageConfirm(message);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
