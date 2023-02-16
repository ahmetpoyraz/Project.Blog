using Microsoft.AspNetCore.Mvc;
using Project.Business.Authentication;
using Project.Business.Services.Lesson;
using Project.Business.Services.Module;
using Project.Entity.Filters.Authentication;
using Project.Entity.Filters.Lesson;
using Project.Entity.Filters.Module;

namespace Project.UI.Controllers
{
    public class DataTableProviderController : BaseController<DataTableProviderController>
    {
         ILessonService _lessonService;
        IFileLinkService _fileLinkService;
        IAuthenticationService _authenticationService;
        public DataTableProviderController(ILessonService lessonService, IFileLinkService fileLinkService, IAuthenticationService authenticationService)
        {
            _lessonService=lessonService;
            _fileLinkService = fileLinkService;
            _authenticationService=authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonList()
        {
            var filter = new LessonFilter() { };
            var result = await _lessonService.GetLessonListAsync(filter);

            if (result.Success)
            {
                var model = new object();
                model = new
                {
                    total = result.Data.Count(),
                    rows = result.Data
                };
                return Ok(model);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonPostList(int lessonId)
        {
            var filter = new LessonPostFilter() { Id = lessonId };
            var result = await _lessonService.GetLessonPostListAsync(filter);
            if (result.Success)
            {
                var model = new object();
                model = new
                {
                    total = result.Data.Count(),
                    rows = result.Data
                };
                return Ok(model);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonPostLinkList(int lessonPostId)
        {
            var filter = new FileLinkFilter() { Id = lessonPostId,ModulePostTypeId = 1,ModulePostTypeDataId=lessonPostId };
            var result = await _fileLinkService.GetFileLinkListByModulePostTypeAndDataId(filter);
            if (result.Success)
            {
                var model = new object();
                model = new
                {
                    total = result.Data.Count(),
                    rows = result.Data
                };
                return Ok(model);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var filter = new UserFilter() {};
            var result = await _authenticationService.GetUserListAsync(filter);
            if (result.Success)
            {
                var model = new object();
                model = new
                {
                    total = result.Data.Count(),
                    rows = result.Data
                };
                return Ok(model);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOperationClaimList(int userId)
        {
            var filter = new UserOperationClaimFilter() { UserId = userId };
            var result = await _authenticationService.GetUserOperationClaimList(filter);
            if (result.Success)
            {
                var model = new object();
                model = new
                {
                    total = result.Data.Count(),
                    rows = result.Data
                };
                return Ok(model);
            }

            return BadRequest(result);
        }

    }
}
