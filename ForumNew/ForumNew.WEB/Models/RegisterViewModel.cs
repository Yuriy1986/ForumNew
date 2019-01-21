using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ForumNew.WEB.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ник*")]
        [MaxLength(25, ErrorMessage = "Ник должен быть не более 25 символов")]
        [MinLength(3, ErrorMessage = "Ник должен быть не менее 3 символов")]
        public string NickName { get; set; }    

        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты*")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль*")]
        [MaxLength(60, ErrorMessage = "Пароль должен быть не более 60 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля*")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}