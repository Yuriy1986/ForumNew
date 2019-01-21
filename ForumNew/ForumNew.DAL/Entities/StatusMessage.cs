using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
