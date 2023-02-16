using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Tables.EvrimDbMSSQL
{
    public class ModulePostLink:BaseEntity
    {
        public int ModuleId { get; set; }
        public int PostId { get; set; }
        public string Link { get; set; }
    }
}
