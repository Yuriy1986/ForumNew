using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ForumNew.DAL.Entities;

namespace ForumNew.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Theme> Themes { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<StatusMessage> StatusMessages { get; set; }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new UserDbInitializer());
        }

        public ApplicationDbContext(string conectionString) : base(conectionString) { }
    }

    public class UserDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            db.StatusMessages.Add(new StatusMessage { StatusMessageText = "Создал сообщение" });
            db.StatusMessages.Add(new StatusMessage { StatusMessageText = "Отредактировал сообщение" });
            db.StatusMessages.Add(new StatusMessage { StatusMessageText = "Удалил сообщение" });
            db.StatusMessages.Add(new StatusMessage { StatusMessageText = "Сообщение удалено админом" });

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            db.Roles.Add(role1);
            db.Roles.Add(role2);
        }
    }
}
