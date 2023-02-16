using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Core.Core.Entities.Result.Concrete;
using Project.Entity.Tables;
using Project.Core.Core.Entities.Result.Abstract;
using Project.Entity.Dtos.Blog;

namespace Project.Business.Blog
{
    public interface IBlogService
    {
        Task<IResult> InsertAsync(BlogDto blogDto);
        Task<IResult> UpdateAsync();
        Task<IResult> DeleteAsync();
        Task<IDataResult<Entity.Tables.EvrimDbMSSQL.Blog>> GetAsync(int id);
        Task<IDataResult<IEnumerable<Entity.Tables.EvrimDbMSSQL.Blog>>> GetAllAsync();
    }
}
