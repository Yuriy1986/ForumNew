using System;
using System.Threading.Tasks;
using ForumNew.DAL.Identity;
using ForumNew.DAL.Entities;

namespace ForumNew.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryMessages Messages { get; }
        IRepositoryThemes Themes { get; }

        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task LogOff(string userId);
        Task Login(ApplicationUser user);
        Task UpdateUsers(string userCurrentId);
    }
}
