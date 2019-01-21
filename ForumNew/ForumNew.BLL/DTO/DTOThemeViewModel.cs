using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumNew.BLL.DTO
{
    public class DTOThemeViewModel
    {
        // Id Theme.
        public int Id { get; set; }

        public string NickName { get; set; }

        public string ThemeText { get; set; }

        // Time of message creation (edition, deletion).
        public DateTime ThemeTime { get; set; }
    }
}
