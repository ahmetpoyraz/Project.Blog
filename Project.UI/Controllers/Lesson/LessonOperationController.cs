using Microsoft.AspNetCore.Mvc;
using Project.Business.Services.Lesson;
using Project.Business.Services.Module;
using Project.Core.Core.Entities.Result.Concrete;
using Project.Core.Enums;
using Project.Entity.Dtos.Lesson;
using Project.Entity.Dtos.Module;
using Project.Entity.Filters.Lesson;
using Project.Entity.Filters.Module;
using System.Text.Json;
using ZendeskApi_v2.Models.Articles;

namespace Project.UI.Controllers.Lesson
{
    public class LessonOperationController : BaseController<LessonOperationController>
    {
        ILessonService _lessonService;
        IFileLinkService _fileLinkService;
        public LessonOperationController(ILessonService lessonService,IFileLinkService fileLinkService)
        {
            _lessonService = lessonService;
            _fileLinkService = fileLinkService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertLessonPost(LessonPostDto dto, List<IFormFile> files)
        {
            dto.UserCreated = GetUserId();
            var result = await _lessonService.InsertLessonPostAsync(dto); 
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> InsertLesson(LessonDto dto)
        {
            dto.UserCreated = GetUserId();
            var result = await _lessonService.InsertLessonAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult InsertFile(IFormFile image)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/src/assets/projectDocuments/lesson/lessonImages");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(image.FileName);
                string fileName = DateTime.Now.Ticks + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return Ok(fileName);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetLessonPostById(int id)
        {
            var result = await _lessonService.GetLessonPostByIdAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLessonPost(LessonPostDto dto)
        {


            dto.UserModified = GetUserId();
            var result = await _lessonService.UpdateLessonPostAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertLessonPostFile(LessonPostDto dto) 
        {
            var fileLinkDtoList = new List<FileLinkDto>();

            if (dto.Files!=null)
            {
                foreach (var item in dto.Files)
                {
                    var fileLinkDto = new FileLinkDto()
                    {
                        File = item,
                        FolderPath = Paths.LESSON_DOCUMENTS,
                        ModulePostTypeId = 1,
                        ModulePostTypeDataId = (int)dto.Id,

                    };
                    fileLinkDtoList.Add(fileLinkDto);
                }
                dto.UserModified = GetUserId();
                var result =await  _fileLinkService.SaveFileListAndInsertDb(fileLinkDtoList);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            return BadRequest(new ErrorResult(System.Net.HttpStatusCode.BadRequest, "Lütfen en az bir dosya seçiniz"));
        


        }


        [HttpGet]
        public async Task<IActionResult> DeleteLesson(int id)
        {
           
            var filter = new LessonFilter() { Id = id, UserId = GetUserId() };
            var result = await _lessonService.DeleteLessonAsync(filter);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLessonPost(int id)
        {

            var filter = new LessonPostFilter() { Id = id, UserId = GetUserId() };
            var result = await _lessonService.DeleteLessonPostAsync(filter);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteLessonPostFile(int id)
        {

            var filter = new FileLinkFilter() { Id = id, UserId = GetUserId() };
            var result = await _fileLinkService.RemoveFile(filter);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var result = await _lessonService.GetLessonByIdAsync(id);
            if (result.Success)
            {
                
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLesson(LessonDto dto)
        {
            dto.UserModified = GetUserId();
            var result = await _lessonService.UpdateLessonAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
