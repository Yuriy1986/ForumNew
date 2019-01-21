using ForumNew.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace ForumNew.DAL.Identity
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
        {
        }
    }
}
