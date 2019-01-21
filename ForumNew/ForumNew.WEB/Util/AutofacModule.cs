using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using ForumNew.BLL.Interfaces;
using ForumNew.BLL.Services;

namespace ForumNew.WEB.Util
{
    public class AutofacModule : Module
    {
        public AutofacModule(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<ThemeService>().As<IThemeService>();

            builder.RegisterType<MessageService>().As<IMessageService>();
        }
    }
}