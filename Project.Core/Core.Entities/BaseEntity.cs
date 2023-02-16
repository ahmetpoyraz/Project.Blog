using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? UserDeleted { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }
    }
}
