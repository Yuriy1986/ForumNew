using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ForumNew.WEB.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "NickName is required.")]
        [Display(Name = "NickName*")]
        [MaxLength(25, ErrorMessage = "The NickName must be no more 25 characters long.")]
        [MinLength(3, ErrorMessage = "The NickName must be at least 3 characters long.")]
        public string NickName { get; set; }    

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        [MaxLength(60, ErrorMessage = "The password must be no more 60 characters long.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password*")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}