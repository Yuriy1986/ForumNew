using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ForumNew.WEB.Models
{
    public class EditMessageViewModel
    {
        [Required]
        public int IdTheme { get; set; }

        [Required]
        public int InternalId { get; set; }

        public string MessageText { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }
    }
}