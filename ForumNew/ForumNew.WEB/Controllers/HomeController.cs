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
//using PagedList;
using System.Text.RegularExpressions;

namespace ForumNew.WEB.Controllers
{
    public class HomeController : Controller
    {
        IUserService UserService;
        private IThemeService ThemeService;
        public HomeController(IUserService service, IThemeService service1)
        {
            UserService = service;
            ThemeService = service1;
        }


        //private IThemeService ThemeService
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<IThemeService>();
        //    }
        //}

        //private IUserService UserService
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<IUserService>();
        //    }
        //}

        //private IThemeService ThemeService
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<IThemeService>();
        //    }
        //}

        private IMessageService MessageService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IMessageService>();
            }
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
            ViewBag.Message = "Простой форум";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Связаться с Админом";
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
                model.ThemeText.Trim();
                model.UserId = User.Identity.GetUserId();

                var createThemeDto = Mapper.Map<DTOCreateThemeViewModel>(model);
                ThemeService.CreateTheme(createThemeDto);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // id - IdTheme.
        [HttpGet]
        //public async Task<ActionResult> ReadMessages(int id, int? page)
        //{
        //    int pageNumber = (page ?? 1);
        //    // Messages on page.
        //    int pageSize = 20;

        //    var messageList = Mapper.Map<IEnumerable<DTOMessageViewModel>, IEnumerable<MessageViewModel>>(MessageService.GetAllMessages(id));
        //    ViewData.Add("IdTheme", id);

        //    if (!Request.IsAjaxRequest())
        //    {
        //        var messageHeader = MessageService.MessageHeader(id);
        //        ViewData.Add("NickCreatorTheme", messageHeader.NickName);
        //        ViewData.Add("ThemeText", messageHeader.ThemeText);
        //        ViewData.Add("ThemeTime", messageHeader.ThemeTime);
        //        return View("ReadMessages", messageList.ToPagedList(pageNumber, pageSize));
        //    }
        //    else
        //    {
        //        if (Request.IsAuthenticated)
        //            await UpdateUsers();
        //        return PartialView("_MessagePartial", messageList.ToPagedList(pageNumber, pageSize));
        //    }
        //}

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
                MessageService.CreateMessage(createMessageDto);

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
                ViewBag.answer = "Вы действительно хотите удалить сообщение: #" + model.InternalId + " пользователя " + nickName;
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
                ViewBag.answer = "Вы действительно хотите удалить сообщение: #" + model.InternalId;
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
                    return "Необходимо указать текст сообщения";
                model.UserId = User.Identity.GetUserId();
                model.MessageText = RegularMessage(model.MessageText);

                var editMessageDto = Mapper.Map<DTOEditMessageViewModel>(model);
                return MessageService.EditMessageConfirm(editMessageDto);
            }
            return "Ошибка (Нажмите кнопку \"Отмена\" для перезагрузки страницы)";
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
    }
}