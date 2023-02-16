using Dapper.Contrib.Extensions;
using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Tables.EvrimDbMSSQL
{
    [Table("FileLinks")]
    public class FileLink : BaseEntity
    {
        public int? ModulePostTypeId { get; set; }
        public int? ModulePostTypeDataId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

    }
}
