using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities.Security
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
