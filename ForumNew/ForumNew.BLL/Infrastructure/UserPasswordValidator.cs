using Microsoft.AspNet.Identity;
using System;

namespace ForumNew.BLL.Infrastructure
{
    public class UserPasswordValidator : PasswordValidator
    {
        public UserPasswordValidator()
        {
            RequiredLength = 6;
            RequireNonLetterOrDigit = true;
            RequireDigit = true;
            RequireLowercase = true;
            RequireUppercase = true;
        }
    }
}
