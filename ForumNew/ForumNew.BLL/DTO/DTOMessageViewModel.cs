﻿using System;

namespace ForumNew.BLL.DTO
{
    public class DTOMessageViewModel
    {
        // Id Message in theme (#1, #2 ...)
        public int InternalId { get; set; }

        public string NickName { get; set; }

        public string UserId { get; set; }

        public bool Online { get; set; }

        public string MessageText { get; set; }

        // Time of message creation (edition, deletion).
        public DateTime MessageTime { get; set; }

        public string StatusMessage { get; set; }

        public int StatusMessageId { get; set; }
    }
}
