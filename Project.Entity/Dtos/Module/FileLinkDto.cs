using Microsoft.AspNetCore.Http;
using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Dtos.Module
{
    public class FileLinkDto: BaseDto
    {
        public int ModulePostTypeId { get; set; }
        public int ModulePostTypeDataId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string FolderPath { get; set; }

        public IFormFile File { get; set; }
    }
}
