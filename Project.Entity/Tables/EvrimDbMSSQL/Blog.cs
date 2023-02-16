using Dapper.Contrib.Extensions;
using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Tables.EvrimDbMSSQL
{
    [Table("Blogs")]
    public class Blog : BaseEntity
    {
        public string Name { get; set; }
    }
}
