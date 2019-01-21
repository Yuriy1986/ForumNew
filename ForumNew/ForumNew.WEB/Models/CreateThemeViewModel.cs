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
        [Required(ErrorMessage = "Необходимо указать название темы")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Наименование темы")]
        [MaxLength(255, ErrorMessage = "Тема должна быть не более 255 символов")]
        public string ThemeText { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }
    }
}