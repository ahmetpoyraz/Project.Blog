using Project.Core.Core.Entities.Result.Abstract;
using Project.Entity.Dtos.Lesson;
using Project.Entity.Filters.Lesson;
using Project.Entity.Procedures.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services.Lesson
{
    public interface ILessonService
    {
        #region LESSON
        Task<IResult> InsertLessonAsync(LessonDto dto);
        Task<IResult> UpdateLessonAsync(LessonDto dto);
        Task<IResult> DeleteLessonAsync(LessonFilter filter);
        Task<IDataResult<LessonDto>> GetLessonByIdAsync(int id);
        Task<IDataResult<IEnumerable<SPLessonList>>> GetLessonListAsync(LessonFilter filter);
        #endregion LESSON

        #region LESSONPOST
        Task<IResult> InsertLessonPostAsync(LessonPostDto dto);

        Task<IDataResult<IEnumerable<LessonPostDto>>> GetAllLessonPostAsync();
        Task<IDataResult<IEnumerable<LessonPostDto>>> GetLessonPostListAsync(LessonPostFilter filter);
        Task<IDataResult<LessonPostDto>> GetLessonPostByIdAsync(int id);
        Task<IResult> UpdateLessonPostAsync(LessonPostDto dto);
        Task<IResult> DeleteLessonPostAsync(LessonPostFilter filter);

        #endregion LESSONPOST


    }
}
