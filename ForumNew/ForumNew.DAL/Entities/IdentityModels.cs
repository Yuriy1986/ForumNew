using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForumNew.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
        public bool Online { get; set; }
        // last login time.
        public System.DateTime TimeLogin { get; set; }
        public ICollection<Theme> Themes { get; set; }
        public ICollection<Message> Messages { get; set; }

        public ApplicationUser()
        {
            Themes = new List<Theme>();
            Messages = new List<Message>();
        }
    }
}
