using Microsoft.AspNetCore.Mvc;
using Project.Business.Authentication;
using Project.Business.Services.Module;
using Project.Core.Enums;
using Project.Entity.Dtos.Authentication;
using Project.Entity.Dtos.Module;
using Project.Entity.Filters.Authentication;
using Project.Entity.Filters.Module;

namespace Project.UI.Controllers.Auth
{
    public class AuthenticationOperationController : BaseController<AuthenticationOperationController>
    {
        IAuthenticationService _authenticationService;
        IFileLinkService _fileLinkService;
        public AuthenticationOperationController(IAuthenticationService authenticationService, IFileLinkService fileLinkService)
        {
            _authenticationService = authenticationService;
            _fileLinkService = fileLinkService;
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var filter = new UserFilter() { Id = id, UserId = GetUserId() };
            var result = await _authenticationService.DeleteUserAsync(filter);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDto dto,IFormFile file)
        {

            dto.UserCreated = GetUserId();
            if (file!=null)
            {
                dto.ProfilPhoto = new FileLinkDto() { File=file};
            }
          
            var result = await _authenticationService.InsertUserAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        public async Task<IActionResult> UserProfilPhoto()
      {
            var fileLinkFilter = new FileLinkFilter()
            {
                ModulePostTypeDataId = GetUserId(),
                ModulePostTypeId = ModulePostTypes.USER_PROFILE_PHOTOS
            };
            var fileLink = await _fileLinkService.GetFileLinkByModulePostTypeAndDataId(fileLinkFilter);

            if (fileLink.Success && fileLink.Data.FilePath != null)
            {
                string profilePicPath = Path.Combine(Directory.GetCurrentDirectory(), fileLink.Data.FilePath);
                return PhysicalFile(profilePicPath, "image/" + fileLink.Data.FileType);
            }
            var profilePicPathNull = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/src/assets/projectDocuments/module/profilePhotos/defult-user.png?c={DateTime.Now}");
            return PhysicalFile(profilePicPathNull, "image/png");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDto dto)
        {
            dto.UserModified = GetUserId();
            var result = await _authenticationService.UpdateUser(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
