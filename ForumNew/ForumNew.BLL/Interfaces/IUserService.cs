using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ForumNew.BLL.DTO;
using ForumNew.BLL.Infrastructure;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace ForumNew.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        // AccountController.

        Task<OperationDetails> Register(DTORegisterViewModel dtoRegisterViewModel);

        Task<ClaimsIdentity> Login(DTOLoginViewModel dtoLoginViewModel);

        string GetUserNickName(IIdentity identity);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);

        Task SendEmailAsync(string userId, string subject, string body);

        Task LogOff(string userId);

        Task<OperationDetails> ForgotPassword(DTOForgotPasswordViewModel dtoForgotPasswordViewModel);

        Task<OperationDetails> ResetPassword(DTOResetPasswordViewModel dtoResetPasswordViewModel);

        Task<OperationDetails> ResetPasswordAsync(string userId, string code, string _password);

        Task UpdateUsers(string userCurrentId);

        // ManageController.

        Task<OperationDetails> ChangePassword(DTOChangePasswordViewModel changePasswordViewModelDto);

        Task<ClaimsIdentity> GetClaim(string userId);
    }
}
