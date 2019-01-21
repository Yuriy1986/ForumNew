using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumNew.DAL.Identity
{
    public class ApplicationRoleManager:RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(RoleStore<IdentityRole> store)
            : base(store)
        { }
    }
}
