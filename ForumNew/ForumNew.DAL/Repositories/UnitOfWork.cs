using ForumNew.DAL.EF;
using ForumNew.DAL.Entities;
using ForumNew.DAL.Interfaces;
using ForumNew.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ForumNew.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext db;

        ApplicationUserManager userManager;

        ApplicationRoleManager roleManager;

        RepositoryMessages repositoryMessages;

        RepositoryThemes repositoryThemes;

        public UnitOfWork(string connectionString)
        {
            db = new ApplicationDbContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(db));
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepositoryMessages Messages
        {
            get
            {
                if (repositoryMessages == null)
                    repositoryMessages = new RepositoryMessages(db);
                return repositoryMessages;
            }
        }

        public IRepositoryThemes Themes
        {
            get
            {
                if (repositoryThemes == null)
                    repositoryThemes = new RepositoryThemes(db);
                return repositoryThemes;
            }
        }

        public async Task LogOff(string userId)
        {
            ApplicationUser user = db.Users.Where(i => i.Id == userId).FirstOrDefault();
            user.Online = false;
            user.TimeLogin = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task Login(ApplicationUser user)
        {
            user.Online = true;
            user.TimeLogin = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task UpdateUsers(string userCurrentId)
        {
            // User is in the database.
            if (userCurrentId != null)
            {
                ApplicationUser user = db.Users.Where(i => i.Id == userCurrentId).FirstOrDefault();
                user.Online = true;
                user.TimeLogin = DateTime.Now;
            }
            // Remove Users (Online=>Offline).
            foreach (var item in db.Users.Where(i => i.Online == true))
            {
                TimeSpan time = DateTime.Now - item.TimeLogin;
                if (time.TotalSeconds > 60)
                {
                    item.Online = false;
                }
            }
            await db.SaveChangesAsync();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
