using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Blog;
using Project.Entity.Tables;
using Project.UI.Models;
using Project.UI.Models.Lesson;

namespace Project.UI.Controllers.Blog
{
    public class BlogController : Controller
    {
       
        IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
          
            return View();
        }
       
        [HttpPost]
        [Authorize]
        public async Task<int> InsertAsync(DenemeModel model)
        {
            //var result = _blogService.InsertAsync(entity);
            return await Task.FromResult(1);
        }
      
    }
}
