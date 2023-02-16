using Project.Entity.Filters.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Lesson
{
    public interface ILessonPostRepository : IGenericRepository<Entity.Tables.EvrimDbMSSQL.LessonPost>
    {
        Task<IEnumerable<Entity.Tables.EvrimDbMSSQL.LessonPost>> GetLessonPostList(LessonPostFilter filter);
        Task DeleteAsync(LessonPostFilter filter);
    }
}
