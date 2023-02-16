using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Blog
{
    public interface IBlogPostRepository : IGenericRepository<Entity.Tables.EvrimDbMSSQL.BlogPost>
    {
    }
}
