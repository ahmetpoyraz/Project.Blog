using Dapper.Contrib.Extensions;
using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Tables.EvrimDbMSSQL
{
    [Table("LessonPosts")]
    public class LessonPost:BaseEntity
    {
        public int LessonId { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
    }
}
