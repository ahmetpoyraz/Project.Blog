using Dapper;
using Project.Core.Core.Entities.Security;
using Project.Entity.Filters.Authentication;
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
    public class OperationClaimRepository : GenericRepository<OperationClaim>, IOperationClaimRepository
    {
        public OperationClaimRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }

        public async Task<IEnumerable<OperationClaim>> GetClaimsByUserIdAsync(int userId)
        {
            string sql = "SP_Users_GetUserClaims";

            return await Connection.QueryAsync<OperationClaim>(sql,
                new
                {
                    userId = userId
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<OperationClaim>> GetOperationClaimList(OperationClaimFilter filter)
        {
            string sql = "SP_GetOperationClaimList";

            return await Connection.QueryAsync<OperationClaim>(sql,
                new
                {
                   
                },
                Transaction,
                commandType: CommandType.StoredProcedure);

        }


    }


    #endregion CONCRETE

    #region ABSTRACT
    public interface IOperationClaimRepository :  IGenericRepository<OperationClaim>
    {
        Task<IEnumerable<OperationClaim>> GetClaimsByUserIdAsync(int userId);
        Task<IEnumerable<OperationClaim>> GetOperationClaimList(OperationClaimFilter filter);
    }

    #endregion CONCRETE
}
