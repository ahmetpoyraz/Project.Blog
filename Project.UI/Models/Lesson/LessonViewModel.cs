using Project.Business.Services.Lesson;
using Project.Entity.Dtos.Lesson;
using Project.Entity.Filters.Lesson;
using Project.Entity.Procedures.Lesson;

namespace Project.UI.Models.Lesson
{
    public class LessonViewModel
    {
        ILessonService _lessonService;
        public IEnumerable<SPLessonList> LessonList { get; set; }
        public LessonDto LessonDetail { get; set; }
        public IEnumerable<LessonPostDto> LessonPostList { get; set; }
        public LessonPostDto LessonPostDetail { get; set; }
   
        public bool IsUpdate { get; set; }=false;

        public LessonViewModel(ILessonService lessonService)
        {

            _lessonService = lessonService;
        }

       public void FillLessonList()
        {
            var filter = new LessonFilter();

            LessonList = _lessonService.GetLessonListAsync(filter).Result.Data;
        }
        public void FillLessonDetail(int id)
        {
            LessonDetail = _lessonService.GetLessonByIdAsync(id).Result.Data;   
        }

        public void FillLessonPostList(int lessonId)
        {
            var filter = new LessonPostFilter() { Id=lessonId};
            LessonPostList = _lessonService.GetLessonPostListAsync(filter).Result.Data;
        }


        public void FillLessonPostDetail(int lessonPostId)
        {
           LessonPostDetail = _lessonService.GetLessonPostByIdAsync(lessonPostId).Result.Data;
            IsUpdate = true;


        }
    }
}
