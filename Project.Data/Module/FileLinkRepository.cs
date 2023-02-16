using Dapper;
using Project.Entity.Filters.Module;
using Project.Entity.Tables.EvrimDbMSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Module
{
    public class FileLinkRepository : GenericRepository<Entity.Tables.EvrimDbMSSQL.FileLink>, IFileLinkRepository
    {
        public FileLinkRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }

        public async Task<IEnumerable<FileLink>> GetFileLinkListByModulePostTypeAndDataId(FileLinkFilter filter)
        {
            string sql = "SP_GetFileLinkListByModulePostTypeAndDataId";

            return await Connection.QueryAsync<FileLink>(sql,
                new
                {
                    modulePostTypeId = filter.ModulePostTypeId,
                    modulePostTypeDataId = filter.ModulePostTypeDataId
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

        public async Task<FileLink> GetFileLinkByModulePostTypeAndDataId(FileLinkFilter filter)
        {
            string sql = "SELECT Top 1 * FROM FileLinks WHERE ModulePostTypeDataId = @modulePostTypeDataId AND ModulePostTypeId =@modulePostTypeId AND IsEnabled=1 ";

            return await Connection.QueryFirstAsync<FileLink>(sql,
                new
                {
                    modulePostTypeId = filter.ModulePostTypeId,
                    modulePostTypeDataId = filter.ModulePostTypeDataId
                },
                Transaction,
                commandType: CommandType.Text);

        }
    }
}
