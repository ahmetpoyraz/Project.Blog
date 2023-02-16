using AutoMapper;
using Project.Core.Core.Entities.Result.Abstract;
using Project.Core.Core.Entities.Result.Concrete;
using Project.Core.Enums;
using Project.Core.Enums.ApiEnum;
using Project.Data;
using Project.Entity.Dtos.Module;
using Project.Entity.Filters.Module;
using Project.Entity.Tables.EvrimDbMSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services.Module
{
    public class FileLinkService : IFileLinkService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public FileLinkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> SaveFileAndInsertDb(FileLinkDto dto)
        {
            try
            {

                string path = Path.Combine(Directory.GetCurrentDirectory(), dto.FolderPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                FileInfo fileInfo = new FileInfo(dto.File.FileName);
                string fileName = DateTime.Now.Ticks + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    dto.File.CopyTo(stream);
                    var entity = new FileLink()
                    {
                        FileName = fileInfo.Name,
                        FilePath = dto.FolderPath + "/" + fileName,
                        FileType = fileInfo.Extension,
                        ModulePostTypeId = dto.ModulePostTypeId,
                        ModulePostTypeDataId = dto.ModulePostTypeDataId

                    };
                    await _unitOfWork.FileLinkRepository.InsertAsync(entity);


                }

                _unitOfWork.Commit();
                var resultText = $"Dosya kaydedildi";
                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.FILE_SAVED);
            }
            catch (Exception ex)
            {

                _unitOfWork.Rollback();
                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public async Task<IResult> SaveFileListAndInsertDb(List<FileLinkDto> dto)
        {
            try
            {
                var successCount = 0;
                foreach (var item in dto)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), item.FolderPath);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);


                    FileInfo fileInfo = new FileInfo(item.File.FileName);
                    string fileName = DateTime.Now.Ticks + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        item.File.CopyTo(stream);
                        var entity = new FileLink()
                        {
                            FileName = fileInfo.Name,
                            FilePath = item.FolderPath + "/" + fileName,
                            FileType = fileInfo.Extension.Replace(".", ""),
                            ModulePostTypeId = item.ModulePostTypeId,
                            ModulePostTypeDataId = item.ModulePostTypeDataId

                        };
                        await _unitOfWork.FileLinkRepository.InsertAsync(entity);
                        successCount++;

                    }
                }


                _unitOfWork.Commit();
                var resultText = $"{successCount} dosya kaydedildi";
                return new SuccessResult(System.Net.HttpStatusCode.OK, resultText);
            }
            catch (Exception ex)
            {

                _unitOfWork.Rollback();
                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public async Task<IDataResult<IEnumerable<FileLinkDto>>> GetFileLinkListByModulePostTypeAndDataId(FileLinkFilter filter)
        {
            try
            {
                var list = await _unitOfWork.FileLinkRepository.GetFileLinkListByModulePostTypeAndDataId(filter);

                var mapped = _mapper.Map<IEnumerable<FileLink>, IEnumerable<FileLinkDto>>(list);

                return new SuccessDataResult<IEnumerable<FileLinkDto>>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<FileLinkDto>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
        public async Task<IDataResult<FileLinkDto>> GetFileLinkByModulePostTypeAndDataId(FileLinkFilter filter)
        {
            try
            {
                var entity = await _unitOfWork.FileLinkRepository.GetFileLinkByModulePostTypeAndDataId(filter);

                var mapped = _mapper.Map<FileLink,FileLinkDto>(entity);

                return new SuccessDataResult<FileLinkDto>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<FileLinkDto>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> RemoveFile(FileLinkFilter filter)
        {
            try
            {
                var successCount = 0;

                var fileLink = await _unitOfWork.FileLinkRepository.GetAsync((int)filter.Id);

                string path = Path.Combine(Directory.GetCurrentDirectory(), fileLink.FilePath);

                File.Delete(path);

                successCount++;

                await _unitOfWork.FileLinkRepository.DeleteAsync((int)filter.Id, (int)filter.UserId);

                _unitOfWork.Commit();
                var resultText = $"{successCount} dosya silindi";

                return new SuccessResult(System.Net.HttpStatusCode.OK, resultText);
            }
            catch (Exception ex)
            {

                _unitOfWork.Rollback();
                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public async Task<IResult> InsertDbWithoutSaveFile(FileLinkDto dto)
        {
            try
            {

                string path = Path.Combine(Directory.GetCurrentDirectory(), dto.FolderPath);

                string fileName = DateTime.Now.Ticks + dto.FileType;

                if (!String.IsNullOrEmpty(dto.FileName))
                {
                    fileName = dto.FileName + dto.FileType;

                }
                string fileNameWithPath = Path.Combine(path, fileName);

                var entity = new FileLink()
                {
                    FileName = dto.FileName,
                    FilePath = dto.FolderPath + "/" + fileName,
                    FileType = dto.FileType,
                    ModulePostTypeId = dto.ModulePostTypeId,
                    ModulePostTypeDataId = dto.ModulePostTypeDataId,

                };
                await _unitOfWork.FileLinkRepository.InsertAsync(entity);



                _unitOfWork.Commit();
                var resultText = $"Dosya kaydedildi";
                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.FILE_SAVED);
            }
            catch (Exception ex)
            {

                _unitOfWork.Rollback();
                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }
    }
}
