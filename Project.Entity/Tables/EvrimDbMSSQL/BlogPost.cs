using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Tables.EvrimDbMSSQL
{
    public class BlogPost: BaseEntity
    {
        public int BlogId { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int ReadTime { get; set; }
        public int ReadCount { get; set; }
    }
}
