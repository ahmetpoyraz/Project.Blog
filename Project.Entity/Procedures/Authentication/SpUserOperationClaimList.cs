using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Procedures.Authentication
{
    public class SpUserOperationClaimList: BaseSP
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
    }
}
