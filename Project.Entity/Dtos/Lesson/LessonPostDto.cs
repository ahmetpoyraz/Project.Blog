using Microsoft.AspNetCore.Http;
using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Dtos.Lesson
{
    public class LessonPostDto : BaseDto
    {
        public int LessonId { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public List<IFormFile> Files{get;set;}
    }
}
