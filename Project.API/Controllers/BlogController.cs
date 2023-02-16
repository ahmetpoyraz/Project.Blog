using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Blog;
using Project.Entity.Dtos.Blog;
using Project.Entity.Tables.EvrimDbMSSQL;
using System.Security.Claims;

namespace Project.API.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : BaseController<BlogController>
    {
        IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpPost("insert")]

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> InsertAsync(BlogDto blogDto)
        {
            blogDto.UserCreated = base.GetUser().UserId;

             var result = await _blogService.InsertAsync(blogDto);
            return Ok(result);
        }
        
    }
}
