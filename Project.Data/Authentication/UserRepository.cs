using Dapper;
using Project.Core.Core.Entities.Security;
using Project.Data.Authentication;
using Project.Entity.Filters.Authentication;
using Project.Entity.Procedures.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Authentication
{
    #region CONCRETE
    public class UserRepository : GenericRepository<Core.Core.Entities.Security.User>, IUserRepository
    {
        public UserRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }

        public async Task<Core.Core.Entities.Security.User> GetByUserNameAsync(string userName)
        {
            string sql = "SELECT * FROM [Users] WHERE UserName=@userName AND IsEnabled=1";

            return await Connection.QueryFirstAsync<Core.Core.Entities.Security.User>(sql, new
            {
                userName = userName,
            }, Transaction);


        }

        public async Task<Core.Core.Entities.Security.User> GetById(int id)
        {
            string sql = "SP_GetUserById";

            return await Connection.QueryFirstAsync<Core.Core.Entities.Security.User>(sql,
                new
                {
                    Id = id
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<SPUserList>> GetUserListAsync(UserFilter filter)
        {
            string sql = "SP_GetUserList";

            return await Connection.QueryAsync<SPUserList>(sql,
                new
                {

                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }


    }
    #endregion CONCRETE


    #region ABSTRACT
    public interface IUserRepository : IGenericRepository<Core.Core.Entities.Security.User>
    {
        Task<Core.Core.Entities.Security.User> GetByUserNameAsync(string userName);
        Task<IEnumerable<SPUserList>> GetUserListAsync(UserFilter filter);
        Task<Core.Core.Entities.Security.User> GetById(int id);
    }
    #endregion ABSTRACT

}
