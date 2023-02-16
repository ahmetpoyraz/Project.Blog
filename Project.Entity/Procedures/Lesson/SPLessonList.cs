using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Procedures.Lesson
{
    public class SPLessonList : BaseSP
    {
        public string Name { get; set; }
        public string CreatedUserFirstName { get; set; }
        public string CreatedUserLastName { get; set; }
    }
}
