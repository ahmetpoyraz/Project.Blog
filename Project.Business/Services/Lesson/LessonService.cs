using AutoMapper;
using Project.Business.Validation.FluentValidation.Dtos.Lesson;
using Project.Core.Core.Entities.Result.Abstract;
using Project.Core.Core.Entities.Result.Concrete;
using Project.Core.CrossCuttingConcerns.Validation;
using Project.Core.Enums.ApiEnum;
using Project.Data;
using Project.Entity.Dtos.Lesson;
using Project.Entity.Filters.Lesson;
using Project.Entity.Procedures.Lesson;
using Project.Entity.Tables.EvrimDbMSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services.Lesson
{
    public class LessonService : ILessonService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region LESSON
        public async Task<IResult> DeleteLessonAsync(LessonFilter filter)
        {
            try
            {
                await _unitOfWork.LessonRepository.DeleteAsync(filter);
                _unitOfWork.Commit();

                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.DELETE);
            }
            catch (Exception ex)
            {

                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<IEnumerable<SPLessonList>>> GetLessonListAsync(LessonFilter filter)
        {
            try
            {
                var list = await _unitOfWork.LessonRepository.GetLessonListAsync(filter);


                return new SuccessDataResult<IEnumerable<SPLessonList>>(System.Net.HttpStatusCode.OK, list, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<SPLessonList>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<LessonDto>> GetLessonByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.LessonRepository.GetById(id);

                var mapped = _mapper.Map<Entity.Tables.EvrimDbMSSQL.Lesson, LessonDto>(entity);

                return new SuccessDataResult<LessonDto>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<LessonDto>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> InsertLessonAsync(LessonDto dto)
        {
            try
            {

                ValidationTool.Validate(new LessonDtoValidator(), dto);
                var entity = _mapper.Map<LessonDto, Entity.Tables.EvrimDbMSSQL.Lesson>(dto);
                var result = await _unitOfWork.LessonRepository.InsertAsync(entity);

                _unitOfWork.Commit();

                return new SuccessDataResult<int>(System.Net.HttpStatusCode.Created, result, OperationTexts.INSERT);
            }
            catch (Exception ex)
            {


                _unitOfWork.Rollback();
                return new ErrorDataResult<int>(System.Net.HttpStatusCode.BadRequest, 0, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> UpdateLessonAsync(LessonDto dto)
        {
            try
            {
              
                ValidationTool.Validate(new LessonDtoValidator(), dto);
                var oldData = await _unitOfWork.LessonRepository.GetById((int)dto.Id);

                var entity = new Entity.Tables.EvrimDbMSSQL.Lesson()
                {
                    Id = oldData.Id,
                    Name = dto.Name,
                    DateCreated = oldData.DateCreated,
                    UserCreated = oldData.UserCreated,
                    IsEnabled = oldData.IsEnabled,
                    DateDeleted = oldData.DateDeleted,
                    UserDeleted = oldData.UserDeleted,
                    UserModified = dto.UserModified

                };
                var result = await _unitOfWork.LessonRepository.UpdateAsync(entity);

                _unitOfWork.Commit();

                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.UPDATE);
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
        #endregion LESSON

        #region LESSONPOST
        public async Task<IDataResult<IEnumerable<LessonPostDto>>> GetAllLessonPostAsync()
        {
            try
            {
                var list = await _unitOfWork.LessonPostRepository.GetAllAsync();

                var mapped = _mapper.Map<IEnumerable<Entity.Tables.EvrimDbMSSQL.LessonPost>, IEnumerable<LessonPostDto>>(list);

                return new SuccessDataResult<IEnumerable<LessonPostDto>>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<LessonPostDto>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<IEnumerable<LessonPostDto>>> GetLessonPostListAsync(LessonPostFilter filter)
        {
            try
            {
                var list = await _unitOfWork.LessonPostRepository.GetLessonPostList(filter);

                var mapped = _mapper.Map<IEnumerable<Entity.Tables.EvrimDbMSSQL.LessonPost>, IEnumerable<LessonPostDto>>(list);

                return new SuccessDataResult<IEnumerable<LessonPostDto>>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<LessonPostDto>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> InsertLessonPostAsync(LessonPostDto dto)
        {
            try
            {

                ValidationTool.Validate(new LessonPostDtoValidator(), dto);
                var entity = _mapper.Map<LessonPostDto, Entity.Tables.EvrimDbMSSQL.LessonPost>(dto);

                var result = await _unitOfWork.LessonPostRepository.InsertAsync(entity);

                _unitOfWork.Commit();

                return new SuccessDataResult<int>(System.Net.HttpStatusCode.Created, result, OperationTexts.INSERT);
            }
            catch (Exception ex)
            {


                _unitOfWork.Rollback();
                return new ErrorDataResult<int>(System.Net.HttpStatusCode.BadRequest, 0, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> UpdateLessonPostAsync(LessonPostDto dto)
        {
            try
            {

                ValidationTool.Validate(new LessonPostDtoValidator(), dto);
                var oldData = await _unitOfWork.LessonPostRepository.GetAsync((int)dto.Id);
                var entity = new Entity.Tables.EvrimDbMSSQL.LessonPost()
                {
                    Id = oldData.Id,
                    LessonId = oldData.LessonId,
                    Caption = dto.Caption,
                    Description = dto.Description,
                    Text = dto.Text,
                    DateCreated = oldData.DateCreated,
                    UserCreated = oldData.UserCreated,
                    IsEnabled = oldData.IsEnabled,
                    DateDeleted = oldData.DateDeleted,
                    UserDeleted = oldData.UserDeleted,
                    UserModified = dto.UserModified,
                    DateModified = oldData.DateModified

                };

                var result = await _unitOfWork.LessonPostRepository.UpdateAsync(entity);

                _unitOfWork.Commit();

                return new SuccessDataResult<bool>(System.Net.HttpStatusCode.OK, result, OperationTexts.UPDATE);
            }
            catch (Exception ex)
            {


                _unitOfWork.Rollback();
                return new ErrorDataResult<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<LessonPostDto>> GetLessonPostByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.LessonPostRepository.GetAsync(id);

                var mapped = _mapper.Map<Entity.Tables.EvrimDbMSSQL.LessonPost, LessonPostDto>(entity);

                return new SuccessDataResult<LessonPostDto>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<LessonPostDto>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> DeleteLessonPostAsync(LessonPostFilter filter)
        {
            try
            {
                await _unitOfWork.LessonPostRepository.DeleteAsync(filter);
                _unitOfWork.Commit();

                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.DELETE);
            }
            catch (Exception ex)
            {

                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }


        #endregion LESSONPOST

      

    }
}
