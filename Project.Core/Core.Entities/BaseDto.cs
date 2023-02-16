using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities
{
    public class BaseDto
    {
        public int? Id { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }

        public int? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
