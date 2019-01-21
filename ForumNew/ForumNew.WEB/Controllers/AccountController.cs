using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ForumNew.WEB.Models;
using ForumNew.BLL.DTO;
using System.Security.Claims;
using ForumNew.BLL.Interfaces;
using ForumNew.BLL.Infrastructure;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace ForumNew.WEB.Controllers
{
    public class AccountController : Controller
    {
        IUserService UserService;
        public AccountController(IUserService service)
        {
            UserService = service;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginDto = Mapper.Map<DTOLoginViewModel>(model);
                ClaimsIdentity claim = await UserService.Login(loginDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            UserService.LogOff(User.Identity.GetUserId());
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerDto = Mapper.Map<DTORegisterViewModel>(model);
                OperationDetails operationDetails = await UserService.Register(registerDto);
                if (operationDetails.Succedeed)
                {
                    string userId = operationDetails.Message;
                    string code = operationDetails.Property;
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId, code }, protocol: Request.Url.Scheme);

                    await UserService.SendEmailAsync(userId, "Подтверждение электронной почты",
"Для завершения регистрации перейдите по ссылке:: <a href=\"" + callbackUrl + "\">завершить регистрацию</a>");

                    return View("DisplayEmail");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserService.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var forgotPasswordDto = Mapper.Map<DTOForgotPasswordViewModel>(model);
                OperationDetails operationDetails = await UserService.ForgotPassword(forgotPasswordDto);

                if (operationDetails.Succedeed)
                {
                    string userId = operationDetails.Message;
                    string code = operationDetails.Property;

                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId, code}, protocol: Request.Url.Scheme);
                    await UserService.SendEmailAsync(userId, "Сброс пароля", "Сбросьте ваш пароль, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                else
                    return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var resetPasswordDto = Mapper.Map<DTOResetPasswordViewModel>(model);
            OperationDetails operationDetails = await UserService.ResetPassword(resetPasswordDto);

            if (!operationDetails.Succedeed)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            string userId = operationDetails.Message;
            OperationDetails operationResetPassword = await UserService.ResetPasswordAsync(userId, model.Code, model.Password);

            if(operationResetPassword.Succedeed)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            ModelState.AddModelError(operationResetPassword.Property, operationResetPassword.Message);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // For _LoginPartial view.
        public string GetUserNickName(IIdentity identity)
        {
            return UserService.GetUserNickName(identity);
        }
    }
}