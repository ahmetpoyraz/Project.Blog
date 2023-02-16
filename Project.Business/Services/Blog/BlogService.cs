using AutoMapper;
using Project.Business.Validation.FluentValidation.Dtos.Blog;
using Project.Core.Core.Entities.Result.Abstract;
using Project.Core.Core.Entities.Result.Concrete;
using Project.Core.CrossCuttingConcerns.Validation;
using Project.Core.Enums.ApiEnum;
using Project.Data;
using Project.Entity.Dtos.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Blog
{
    public class BlogService : IBlogService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<IResult> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<IEnumerable<Entity.Tables.EvrimDbMSSQL.Blog>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Entity.Tables.EvrimDbMSSQL.Blog>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> InsertAsync(BlogDto blogDto)
        {
            try
            {

                ValidationTool.Validate(new BlogDtoValidator(), blogDto);
                var entity = _mapper.Map<BlogDto,Entity.Tables.EvrimDbMSSQL.Blog>(blogDto);
                var result = await _unitOfWork.BlogRepository.InsertAsync(entity);

                _unitOfWork.Commit();

                return new SuccessDataResult<int>(System.Net.HttpStatusCode.Created,result,OperationTexts.INSERT);
            }
            catch (Exception ex)
            {
                

                _unitOfWork.Rollback();
                return new ErrorDataResult<int>(System.Net.HttpStatusCode.BadRequest,0,ex.Message);
               
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public Task<IResult> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
