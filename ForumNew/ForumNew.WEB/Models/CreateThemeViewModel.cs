using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumNew.WEB.Models
{
    public class CreateThemeViewModel
    {
        [Required(ErrorMessage = "Message text is required.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Your message:")]
        public string ThemeText { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }
    }
}