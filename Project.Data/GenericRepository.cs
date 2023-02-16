using Dapper.Contrib.Extensions;
using Project.Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public SqlConnection Connection;

        public IDbTransaction Transaction;

        public GenericRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            Transaction = dbTransaction;
            Connection = sqlConnection;
        }
        public  async Task<bool> DeleteAsync(int id,int userId)
        {
            var entity = await this.GetAsync(id);
            entity.DateDeleted = DateTime.Now;
            entity.UserDeleted = userId;
            entity.IsEnabled = false;

            return await Connection.UpdateAsync(entity, Transaction);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Connection.GetAllAsync<T>(Transaction);
        }

        public async Task<T> GetAsync(int id)
        {
            return await Connection.GetAsync<T>(id, Transaction);

        }

        public async Task<int> InsertAsync(T entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.IsEnabled = true;

            return await Connection.InsertAsync<T>(entity,Transaction);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.DateModified = DateTime.Now;
            
            return await Connection.UpdateAsync<T>(entity, Transaction);
        }
    }
}
