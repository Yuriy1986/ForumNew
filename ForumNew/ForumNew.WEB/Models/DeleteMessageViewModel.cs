using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumNew.WEB.Models
{
    public class DeleteMessageViewModel
    {
        [Required]
        public int IdTheme { get; set; }

        [Required]
        public int InternalId { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }
    }
}