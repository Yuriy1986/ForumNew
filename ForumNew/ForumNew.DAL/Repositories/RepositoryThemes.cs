using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ForumNew.DAL.EF;
using ForumNew.DAL.Entities;
using ForumNew.DAL.Interfaces;

namespace ForumNew.DAL.Repositories
{
    public class RepositoryThemes : IRepositoryThemes
    {
        ApplicationDbContext db;

        public RepositoryThemes(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Theme> GetAllThemes()
        {
            return db.Themes.Include(nick => nick.ApplicationUser);
        }

        public void CreateTheme(Theme theme)
        {
            db.Themes.Add(theme);
            db.Entry(theme).State = EntityState.Added;
            db.SaveChanges();
        }

        public void DeleteTheme(int id)
        {
            Theme themeCurrent = db.Themes.FirstOrDefault(x => x.Id == id);
            if (themeCurrent!=null)
            {
                db.Themes.Remove(themeCurrent);
                db.Entry(themeCurrent).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
