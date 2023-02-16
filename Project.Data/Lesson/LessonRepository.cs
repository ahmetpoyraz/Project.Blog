using Dapper;
using Project.Data.Lesson;
using Project.Entity.Filters.Lesson;
using Project.Entity.Procedures.Lesson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Lesson
{
    public class LessonRepository : GenericRepository<Entity.Tables.EvrimDbMSSQL.Lesson>, ILessonRepository
    {
        public LessonRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }

        public async Task<IEnumerable<SPLessonList>> GetLessonListAsync(LessonFilter filter)
        {
            string sql = "SP_GetLessonList";

            return await Connection.QueryAsync<SPLessonList>(sql,
                new
                {
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

        public async Task DeleteAsync(LessonFilter filter)
        {
            string sql = "SP_DeleteLesson";

            await Connection.QueryAsync(sql,
                new
                {
                    Id = filter.Id,
                    UserDeleted=filter.UserId
                    
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

        public async Task<Entity.Tables.EvrimDbMSSQL.Lesson> GetById(int id)
        {
            string sql = "SP_GetLessonById";

            return await Connection.QueryFirstAsync<Entity.Tables.EvrimDbMSSQL.Lesson>(sql,
                new
                {
                    Id=id
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }
    }
}
