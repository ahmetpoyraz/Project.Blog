using Project.Core.Core.Entities.Result.Abstract;
using Project.Entity.Dtos.Module;
using Project.Entity.Filters.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services.Module
{
    public interface IFileLinkService
    {
        Task<IResult> SaveFileAndInsertDb(FileLinkDto dto);
        Task<IResult> SaveFileListAndInsertDb(List<FileLinkDto> dto);
        Task<IDataResult<IEnumerable<FileLinkDto>>> GetFileLinkListByModulePostTypeAndDataId(FileLinkFilter filter);
        Task<IDataResult<FileLinkDto>> GetFileLinkByModulePostTypeAndDataId(FileLinkFilter filter);
         Task<IResult> RemoveFile(FileLinkFilter filter);
        Task<IResult> InsertDbWithoutSaveFile(FileLinkDto dto);
    }
}
