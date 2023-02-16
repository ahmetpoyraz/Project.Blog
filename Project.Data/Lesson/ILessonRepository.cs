using Project.Entity.Filters.Lesson;
using Project.Entity.Procedures.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Lesson
{
    public interface ILessonRepository : IGenericRepository<Entity.Tables.EvrimDbMSSQL.Lesson>
    {
        Task<IEnumerable<SPLessonList>> GetLessonListAsync(LessonFilter filter);
        Task DeleteAsync(LessonFilter filter);
        Task<Entity.Tables.EvrimDbMSSQL.Lesson> GetById(int id);
    }
}
