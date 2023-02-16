using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Filters.Module
{
    public class FileLinkFilter : BaseFilter
    {
        public int ModulePostTypeId { get; set; }
        public int ModulePostTypeDataId { get; set; }
    }
}
