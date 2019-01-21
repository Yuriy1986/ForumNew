﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumNew.DAL.Entities
{
    public class Theme
    {
        public int Id { get; set; } 

        public string ApplicationUserId { get; set; } 

        public ApplicationUser ApplicationUser { get; set; }

        public string ThemeText { get; set; }

        public DateTime ThemeTime { get; set; } 

        public ICollection<Message> Messages { get; set; }

        public Theme()
        {
            Messages = new List<Message>();
        }
    }
}
