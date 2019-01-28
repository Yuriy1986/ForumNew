using System;
using System.Collections.Generic;

namespace ForumNew.DAL.Entities
{
    public class StatusMessage
    {
        // 1 - Create message.
        // 2 - Edit message.
        // 3 - Remove message.
        // 4 - Message removed by admin.

        public int Id { get; set; }

        public string StatusMessageText { get; set; }

        public ICollection<Message> Messages { get; set; }

        public StatusMessage()
        {
            Messages = new List<Message>();
        }
    }
}
