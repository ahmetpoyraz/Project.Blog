using Project.Entity.Filters.Module;
using Project.Entity.Tables.EvrimDbMSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Module
{
    public interface IFileLinkRepository : IGenericRepository<Entity.Tables.EvrimDbMSSQL.FileLink>
    {
        Task<IEnumerable<FileLink>> GetFileLinkListByModulePostTypeAndDataId(FileLinkFilter filter);
        Task<FileLink> GetFileLinkByModulePostTypeAndDataId(FileLinkFilter filter);
    }
}
