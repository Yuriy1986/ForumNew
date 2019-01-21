using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumNew.DAL.Entities
{
    public class Message
    {
        // Id Message.
        public int Id { get; set; } 

        // Id Message in theme (#1, #2 ...).
        public int InternalId { get; set; } 

        public string ApplicationUserId { get; set; } 

        public ApplicationUser ApplicationUser { get; set; }

        public int ThemeId { get; set; }

        public Theme Theme { get; set; }

        public string MessageText { get; set; }

        // Time of message creation (edition, deletion).
        public DateTime MessageTime { get; set; } 

        public StatusMessage StatusMessage { get; set; }

        public int StatusMessageId { get; set; }
    }
}
