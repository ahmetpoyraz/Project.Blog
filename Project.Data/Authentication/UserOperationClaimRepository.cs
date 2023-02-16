using Project.Core.Core.Entities.Security;
using Project.Entity.Filters.Authentication;
using Project.Entity.Procedures.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Project.Data.Authentication
{
    #region CONCRETE

    public class UserOperationClaimRepository : GenericRepository<UserOperationClaim>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }

        public async Task<IEnumerable<SpUserOperationClaimList>> GetUserOperationClaimList(UserOperationClaimFilter filter)
        {
            string sql = "SP_GetUserOperationClaimList";

            return await Connection.QueryAsync<SpUserOperationClaimList>(sql,
                new
                {
                    UserId = filter.UserId
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

    }
    #endregion CONCRETE

    #region ABSTRACT
    public interface IUserOperationClaimRepository : IGenericRepository<UserOperationClaim>
    {
        Task<IEnumerable<SpUserOperationClaimList>> GetUserOperationClaimList(UserOperationClaimFilter filter);

    }
    #endregion ABSTRACT

}
