using AutoMapper;
using ForumNew.BLL.DTO;
using ForumNew.DAL.Entities;
using ForumNew.WEB.Models;
using System;

namespace ForumNew.WEB.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(
            cfg =>
            {
                //BLL.
                // ThemeService / GetAllThemes.
                cfg.CreateMap<Theme, DTOThemeViewModel>()
                    .ForMember("NickName", opt => opt.MapFrom(src => src.ApplicationUser.NickName));

                // ThemeService / CreateTheme.
                cfg.CreateMap<DTOCreateThemeViewModel, Theme>()
                    .ForMember("ApplicationUserId", opt => opt.MapFrom(src => src.UserId))
                    .ForMember("ThemeTime", opt => opt.MapFrom(x=>DateTime.Now));

                // MessageService / MessageHeader.
                cfg.CreateMap<Theme, DTOMessageHeader>()
                    .ForMember("NickName", opt => opt.MapFrom(src => src.ApplicationUser.NickName));

                // MessageService / GetAllMessages.
                cfg.CreateMap<Message, DTOMessageViewModel>()
                    .ForMember("NickName", opt => opt.MapFrom(src => src.ApplicationUser.NickName))
                    .ForMember("UserId", opt => opt.MapFrom(src => src.ApplicationUserId))
                    .ForMember("Online", opt => opt.MapFrom(src => src.ApplicationUser.Online))
                    .ForMember("StatusMessage", opt => opt.MapFrom(src => src.StatusMessage.StatusMessageText));

                // MessageService / CreateMessage.
                cfg.CreateMap<DTOCreateMessageViewModel, Message>()
                    .ForMember("ApplicationUserId", opt => opt.MapFrom(src => src.UserId))
                    .ForMember("ThemeId", opt => opt.MapFrom(src => src.IdTheme))
                    .ForMember("MessageTime", opt => opt.MapFrom(x=>DateTime.Now));

                // MessageService / DeleteMessageAdmin.
                cfg.CreateMap<DTODeleteMessageViewModel, Message>()
                    .ForMember("ThemeId", opt => opt.MapFrom(src => src.IdTheme))
                    .ForMember("ApplicationUserId", opt => opt.MapFrom(src => src.UserId));

                // MessageService / EditMessage, EditMessageConfirm.
                cfg.CreateMap<DTOEditMessageViewModel, Message>()
                    .ForMember("ThemeId", opt => opt.MapFrom(src => src.IdTheme))
                    .ForMember("ApplicationUserId", opt => opt.MapFrom(src => src.UserId));

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //WEB.
                // Account / Login.
                cfg.CreateMap<LoginViewModel, DTOLoginViewModel>();

                // Account / Register.
                cfg.CreateMap<RegisterViewModel, DTORegisterViewModel>();

                // Account / ForgotPassword.
                cfg.CreateMap<ForgotPasswordViewModel, DTOForgotPasswordViewModel>();

                // Account / ResetPassword.
                cfg.CreateMap<ResetPasswordViewModel, DTOResetPasswordViewModel>();

                // Manage/ChangePassword.
                cfg.CreateMap<ChangePasswordViewModel, DTOChangePasswordViewModel>();

                // Home / Index.
                cfg.CreateMap<DTOThemeViewModel, ThemeViewModel>();

                // Home / CreateTheme.
                cfg.CreateMap<CreateThemeViewModel, DTOCreateThemeViewModel>();

                //Home / ReadMessages.
                cfg.CreateMap<DTOMessageViewModel, MessageViewModel>();

                //Home / CreateMessage.
                cfg.CreateMap<CreateMessageViewModel, DTOCreateMessageViewModel>();

                //Home / DeleteMessageAdmin, DeleteMessageConfirm.
                cfg.CreateMap<DeleteMessageViewModel, DTODeleteMessageViewModel>();

                //Home / EditMessage, EditMessageConfirm.
                cfg.CreateMap<EditMessageViewModel, DTOEditMessageViewModel>();
            }
            );
        }
    }
}