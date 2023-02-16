using Microsoft.AspNetCore.Mvc;
using Project.Core.Core.Entities.Security;
using System.Security.Claims;

namespace Project.API.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        [NonAction]
        public UserSession GetUser()
        {
          

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role).ToList();
            var roles = new List<string>();

            foreach (var role in roleClaims)
            {
                roles.Add(role.Value);
            }
            return new UserSession()
            {
                UserId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = HttpContext.User.FindFirstValue(ClaimTypes.Name),
                Roles = roles
            };

        }
    }
}

