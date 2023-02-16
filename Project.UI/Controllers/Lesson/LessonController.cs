using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Services.Lesson;
using Project.Entity.Dtos.Lesson;
using Project.UI.Models.Lesson;

namespace Project.UI.Controllers.Lesson
{

    public class LessonController : Controller
    {
        ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        public IActionResult Index()
        {

            var lessonViewModel = new LessonViewModel(_lessonService);
            lessonViewModel.FillLessonList();   

            return View(lessonViewModel);
        }

        public IActionResult Posts(int id)
        {
            var lessonViewModel = new LessonViewModel(_lessonService);
            lessonViewModel.FillLessonDetail(id);
            lessonViewModel.FillLessonPostList(id);

            return View(lessonViewModel);
        }

        public IActionResult PostDetails(int id)
        {
            var lessonViewModel = new LessonViewModel(_lessonService);
            lessonViewModel.FillLessonPostDetail(id);


            return View(lessonViewModel);
        }

        //[Authorize]
        //public IActionResult CreateOrUpdate(int? id)
        //{
        //    var lessonViewModel = new LessonViewModel(_lessonService);
        //    if (id !=null)
        //    {
        //        lessonViewModel.FillLessonPostDetail((int)id);

        //    }
           
        //    lessonViewModel.FillLessonList();

        //    return View(lessonViewModel);
        //}

        public IActionResult CreateOrUpdateLessonPost()
        {

            return View();
        }


    }
}
