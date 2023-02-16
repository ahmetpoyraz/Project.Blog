using Project.Data.Blog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Blog
{
    public class BlogPostRepository : GenericRepository<Entity.Tables.EvrimDbMSSQL.BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction)
        {
        }
    }
}
