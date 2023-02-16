using Dapper;
using Project.Data.Lesson;
using Project.Entity.Filters.Lesson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Lesson
{
    public class LessonPostRepository : GenericRepository<Entity.Tables.EvrimDbMSSQL.LessonPost>, ILessonPostRepository
    {
        public LessonPostRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }
        public async Task<IEnumerable<Entity.Tables.EvrimDbMSSQL.LessonPost>> GetLessonPostList(LessonPostFilter filter )
        {
            string sql = "SP_GetLessonPostList";

            return await Connection.QueryAsync<Entity.Tables.EvrimDbMSSQL.LessonPost>(sql,
                new
                {
                    lessonId = filter.Id
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }
        public async Task DeleteAsync(LessonPostFilter filter)
        {
            string sql = "SP_DeleteLessonPost";

            await Connection.QueryAsync(sql,
                new
                {
                    Id = filter.Id,
                    UserDeleted = filter.UserId

                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

  

    }
}
