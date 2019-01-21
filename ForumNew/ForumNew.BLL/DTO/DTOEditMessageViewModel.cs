using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumNew.BLL.DTO
{
    public class DTOEditMessageViewModel
    {
        public int IdTheme { get; set; }

        public int InternalId { get; set; }

        public string MessageText { get; set; }

        public string UserId { get; set; }
    }
}
