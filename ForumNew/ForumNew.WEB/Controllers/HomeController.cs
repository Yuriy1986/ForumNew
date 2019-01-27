using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ForumNew.WEB.Models;
using ForumNew.BLL.DTO;
using ForumNew.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Text.RegularExpressions;

namespace ForumNew.WEB.Controllers
{
    public class HomeController : Controller
    {
        IUserService UserService;
        IThemeService ThemeService;
        IMessageService MessageService;
        public HomeController(IUserService userService, IThemeService themeService, IMessageService messageService)
        {
            UserService = userService;
            ThemeService = themeService;
            MessageService = messageService;
        }

        public async Task<ActionResult> Index()
        {
            var themes = Mapper.Map<IEnumerable<DTOThemeViewModel>, IEnumerable<ThemeViewModel>>(ThemeService.GetAllThemes());

            if (!Request.IsAjaxRequest())
            {
                return View(themes);
            }

            else
            {
                if (Request.IsAuthenticated)
                    await UpdateUsers();
                return PartialView("_ThemePartial", themes);
            }
        }

        [Authorize]
        public async Task UpdateUsers()
        {
            await UserService.UpdateUsers(User.Identity.GetUserId());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Simple forum ASP.NET MVC5";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact admin";
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateTheme()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateTheme(CreateThemeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Regex regex = new Regex(@"\r\n");
                model.ThemeText = regex.Replace(model.ThemeText, "");
                model.ThemeText= model.ThemeText.Trim();
                model.UserId = User.Identity.GetUserId();

                var createThemeDto = Mapper.Map<DTOCreateThemeViewModel>(model);
                ThemeService.CreateTheme(createThemeDto);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // id - IdTheme.
        [HttpGet]
        public async Task<ActionResult> ReadMessages(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            // Messages on page.
            int pageSize = 20;

            var messageList = Mapper.Map<IEnumerable<DTOMessageViewModel>, IEnumerable<MessageViewModel>>
                (MessageService.GetAllMessages(id, ref pageNumber, pageSize, out int totalPages));
            ViewData.Add("IdTheme", id);
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            if (!Request.IsAjaxRequest())
            {
                var messageHeader = MessageService.MessageHeader(id);
                if(messageHeader==null)
                    return RedirectToAction("Index");

                ViewData.Add("NickCreatorTheme", messageHeader.NickName);
                ViewData.Add("ThemeText", messageHeader.ThemeText);
                ViewData.Add("ThemeTime", messageHeader.ThemeTime);
                return View("ReadMessages", messageList);
            }
            else
            {
                if (Request.IsAuthenticated)
                    await UpdateUsers();
                return PartialView("_MessagePartial", messageList);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateMessage(CreateMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.MessageText = RegularMessage(model.MessageText);
                model.UserId = User.Identity.GetUserId();
                var createMessageDto = Mapper.Map<DTOCreateMessageViewModel>(model);
                if(!MessageService.CreateMessage(createMessageDto))
                    return HttpNotFound();

                return RedirectToAction("ReadMessages", new { id = model.IdTheme, page = model.Page });
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteMessageAdmin(DeleteMessageViewModel model, string nickName)
        {
            if (ModelState.IsValid)
            {
                ViewBag.answer = "Are you sure you want to delete message: #" + model.InternalId + " user`s " + nickName;
                return PartialView("_DeleteMessageAdminPartial", model);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteMessageAdmin(DeleteMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var deleteMessageDto = Mapper.Map<DTODeleteMessageViewModel>(model);
                return MessageService.DeleteMessage(deleteMessageDto);
            }
            return false;
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteMessage(DeleteMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.answer = "Are you sure you want to delete message: #" + model.InternalId;
                return PartialView("_DeleteMessagePartial", model);
            }
            return HttpNotFound();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteMessageConfirm(DeleteMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = User.Identity.GetUserId();
                var deleteMessageDto = Mapper.Map<DTODeleteMessageViewModel>(model);
                return MessageService.DeleteMessage(deleteMessageDto);
            }
            return false;
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditMessage(EditMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editMessageDto = Mapper.Map<DTOEditMessageViewModel>(model);
                model.MessageText = MessageService.GetMessage(editMessageDto);
                if (model.MessageText == null)
                    return HttpNotFound();
                return PartialView("_EditMessagePartial", model);
            }
            return HttpNotFound();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string EditMessageConfirm(EditMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MessageText == null)
                    return "Message text is required.";
                model.UserId = User.Identity.GetUserId();
                model.MessageText = RegularMessage(model.MessageText);

                var editMessageDto = Mapper.Map<DTOEditMessageViewModel>(model);
                return MessageService.EditMessageConfirm(editMessageDto);
            }
            return "Error (Click \"Cancel\" to reload page)";
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTheme(int id)
        {
            ThemeService.DeleteTheme(id);
            return RedirectToAction("Index");
        }

        private string RegularMessage(string messageText)
        {
            Regex regex = new Regex(@"(\s)*$", RegexOptions.Multiline);
            messageText = regex.Replace(messageText, "");
            regex = new Regex(@"^(\s)*", RegexOptions.Multiline);
            messageText = regex.Replace(messageText, "");
            return messageText;
        }

        protected override void Dispose(bool disposing)
        {
            UserService.Dispose();
            ThemeService.Dispose();
            MessageService.Dispose();
            base.Dispose(disposing);
        }
    }
}