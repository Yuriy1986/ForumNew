using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumNew.WEB.Models
{
    public class ThemeViewModel
    {
        // Id Theme.
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "NickName")]
        public string NickName { get; set; }

        [Display(Name = "Theme")]
        public string ThemeText { get; set; }

        [Display(Name = "Date of creation")]
        public DateTime ThemeTime { get; set; }
    }
}