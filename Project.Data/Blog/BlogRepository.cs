using Project.Data.Blog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entity.Tables;
using Dapper;

namespace Project.Data.Blog
{
    public class BlogRepository : GenericRepository<Entity.Tables.EvrimDbMSSQL.Blog>, IBlogRepository
    {
        public BlogRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {

        }



        //public new Task<int> InsertAsync(Entity.Tables.EvrimDbMSSQL.Blog entity)
        //{
        //    return base.InsertAsync(entity);
        //}

    }
}
