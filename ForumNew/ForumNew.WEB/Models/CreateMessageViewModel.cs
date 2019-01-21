using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumNew.WEB.Models
{
    public class CreateMessageViewModel
    {
        [Required]
        public int IdTheme { get; set; }

        public string MessageText { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public int Page { get; set; }
    }
}