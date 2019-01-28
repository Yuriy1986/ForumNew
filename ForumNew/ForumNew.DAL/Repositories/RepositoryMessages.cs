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

        public IEnumerable<Message> GetAllMessages(int id, ref int pageNumber, int pageSize, out int totalPages)
        {
            var selection = db.Messages.Where(i => i.ThemeId == id).OrderBy(o => o.InternalId)
                .Include(src => src.ApplicationUser)
                .Include(src => src.StatusMessage)
                .ToList();

            totalPages = (int)Math.Ceiling((decimal)selection.Count() / pageSize);

            if (totalPages < 1)
                totalPages = 1;

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageNumber > totalPages)
                pageNumber = totalPages;

            return selection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public bool CreateMessage(Message message)
        {
            Theme themeCurrent = db.Themes.FirstOrDefault(x => x.Id == message.ThemeId);
            if (themeCurrent == null)
                return false;

            message.InternalId = db.Messages.Where(i => i.ThemeId == themeCurrent.Id).Count() + 1;
            message.StatusMessageId = 1;
            db.Messages.Add(message);
            db.Entry(message).State = EntityState.Added;
            db.SaveChanges();
            return true;

        }

        public bool DeleteMessage(Message message)
        {
            Message mesCurrent = db.Messages.FirstOrDefault(x => x.InternalId == message.InternalId && x.ThemeId == message.ThemeId);

            if (mesCurrent == null || mesCurrent.StatusMessageId == 3 || mesCurrent.StatusMessageId == 4)
                return false;

            // DeleteMessageConfirm.
            if (message.ApplicationUserId != null && message.ApplicationUserId != mesCurrent.ApplicationUserId)
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
            Message mesCurrent = db.Messages.FirstOrDefault(x => x.InternalId == message.InternalId && x.ThemeId == message.ThemeId);

            if (mesCurrent == null || mesCurrent.StatusMessageId == 3 || mesCurrent.StatusMessageId == 4)
                return null;
            else
                return mesCurrent.MessageText;
        }

        public string EditMessageConfirm(Message message)
        {
            Message mesCurrent = db.Messages.FirstOrDefault(x => x.InternalId == message.InternalId && x.ThemeId == message.ThemeId);

            if (mesCurrent == null || mesCurrent.StatusMessageId == 3 || mesCurrent.StatusMessageId == 4 || mesCurrent.ApplicationUserId != message.ApplicationUserId)
                return "Error (Click button \"Cancel\" to reload the page.)";

            if (message.MessageText == mesCurrent.MessageText)
                return "Editable message is the same as the original.";

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
