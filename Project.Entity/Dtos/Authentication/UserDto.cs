using Project.Core.Core.Entities;
using Project.Entity.Dtos.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Dtos.Authentication
{
    public class UserDto:BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public FileLinkDto ProfilPhoto { get; set; }
        public IEnumerable<OperationClaimDto> OperationClaims { get; set; }

    }
}
