using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Procedures.Authentication
{
    public class SPUserList : BaseSP
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
        public string ProfilePhoto { get; set; }

    }
}
