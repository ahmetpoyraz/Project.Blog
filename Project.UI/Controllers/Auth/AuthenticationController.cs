using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Authentication;
using Project.Business.Services.Module;
using Project.Core.Enums;
using Project.Entity.Dtos.Authentication;
using Project.Entity.Filters.Module;
using Project.UI.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.UI.Controllers.Auth
{
    public class  AuthenticationController : Controller
    {
        Business.Authentication.IAuthenticationService _authenticationService;
        IFileLinkService _fileLinkService;
        public AuthenticationController(Business.Authentication.IAuthenticationService authenticationService, IFileLinkService fileLinkService)
        {
            _authenticationService = authenticationService;
            _fileLinkService = fileLinkService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Lesson");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var result = await _authenticationService.Login(loginDto);

            if (result.Success)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(1);


                cookieOptions.Path = "/";
                Response.Cookies.Append("jwtToken", result.Data.Token, cookieOptions);


                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(result.Data.Token);
                var claimValue = securityToken.Claims.ToList();

                var identity = new ClaimsIdentity(claimValue,
                     CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(identity));
                int id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());




                var fileLinkFilter = new FileLinkFilter()
                {
                    ModulePostTypeId = ModulePostTypes.USER_PROFILE_PHOTOS,
                    ModulePostTypeDataId = Convert.ToInt32(claimValue.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault())
                };
                var profileLink = await _fileLinkService.GetFileLinkByModulePostTypeAndDataId(fileLinkFilter);


                Response.Cookies.Append("profilePhotoUrl", profileLink.Data.FilePath);


                return Redirect("/Admin/Index");
            }

            return BadRequest(result);


        }

        public async Task<IActionResult> Logout()
        {


            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Lesson");

        }

        public IActionResult Users()
        {


            return View();
        }

        public IActionResult UserDetail(int id)
        {
            var authenticationViewModel = new AuthenticationViewModel(_authenticationService);
            authenticationViewModel.FillUserDetail(id);

            return View(authenticationViewModel);
        }

        private async Task<string> GetProfilPhoto(int id)
        {
            var fileLinkFilter = new FileLinkFilter()
            {
                ModulePostTypeDataId = id,
                ModulePostTypeId = ModulePostTypes.USER_PROFILE_PHOTOS
            };
            var fileLink = await _fileLinkService.GetFileLinkByModulePostTypeAndDataId(fileLinkFilter);

            if (fileLink.Success && fileLink.Data.FilePath != null)
            {
                string profilePicPath = Path.Combine(Directory.GetCurrentDirectory(), fileLink.Data.FilePath);
                return profilePicPath+ "image/" + fileLink.Data.FileType;
            }
           return  Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/src/assets/projectDocuments/module/profilePhotos/defult-user.png?c={DateTime.Now}");

        }

    }
}
