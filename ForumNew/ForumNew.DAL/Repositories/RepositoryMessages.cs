using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ForumNew.DAL.EF;
using ForumNew.DAL.Entities;
using ForumNew.DAL.Interfaces;

namespace ForumNew.DAL.Repositories
{
    public class RepositoryMessages : IRepositoryMessages
    {
        ApplicationDbContext db;

        public RepositoryMessages(ApplicationDbContext context)
        {
            this.db = context;
        }

        public Theme MessageHeader(int id)
        {
            return db.Themes.Where(i => i.Id == id).Include(src => src.ApplicationUser).FirstOrDefault();
        }

        public IEnumerable<Message> GetAllMessages(int id)
        {
            return db.Messages.Where(i => i.ThemeId == id).OrderBy(o=>o.InternalId)
                .Include(src => src.ApplicationUser)
                .Include(src => src.StatusMessage);
        }

        public void CreateMessage(Message message)
        {
            message.InternalId = db.Messages.Where(i => i.ThemeId == message.ThemeId).Count() + 1;
            message.StatusMessageId = 1;
            db.Messages.Add(message);
            db.Entry(message).State = EntityState.Added;
            db.SaveChanges();
        }

        public bool DeleteMessage(Message message)
        {
            Message mesCurrent = db.Messages.Where(x => x.InternalId == message.InternalId && x.ThemeId == message.ThemeId).FirstOrDefault();

            if (mesCurrent == null || mesCurrent.StatusMessageId == 3 || mesCurrent.StatusMessageId == 4)
                return false;

             // DeleteMessageConfirm.
            if (message.ApplicationUserId!=null && message.ApplicationUserId!=mesCurrent.ApplicationUserId)
                return false;

            if (message.ApplicationUserId == null)
                mesCurrent.StatusMessageId = 4;
            else
                mesCurrent.StatusMessageId = 3;

            mesCurrent.MessageText = "-";
            mesCurrent.MessageTime = DateTime.Now;
            db.Entry(mesCurrent).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public string GetMessage(Message message)
        {
            Message mesCurrent = db.Messages.Where(x => x.InternalId == message.InternalId && x.ThemeId == message.ThemeId).FirstOrDefault();
            if (mesCurrent == null || mesCurrent.StatusMessageId == 3 || mesCurrent.StatusMessageId == 4)
                return null;
            else
                return mesCurrent.MessageText;
        }

        public string EditMessageConfirm(Message message)
        {
            Message mesCurrent = db.Messages.Where(x => x.InternalId == message.InternalId && x.ThemeId == message.ThemeId).FirstOrDefault();

            if (mesCurrent == null || mesCurrent.StatusMessageId == 3 || mesCurrent.StatusMessageId == 4 || mesCurrent.ApplicationUserId!=message.ApplicationUserId)
                return "Ошибка (Нажмите кнопку \"Отмена\" для перезагрузки страницы)";

            if (message.MessageText== mesCurrent.MessageText)
                return "Редактируемое сообщение совпадает с исходным";
            
            else
            {
                mesCurrent.StatusMessageId = 2;
                mesCurrent.MessageText = message.MessageText;
                mesCurrent.MessageTime = DateTime.Now;
                db.Entry(mesCurrent).State = EntityState.Modified;
                db.SaveChanges();
                return "True";
            }
        }
    }
}
