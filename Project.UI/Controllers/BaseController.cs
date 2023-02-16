using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;

namespace Project.UI.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    { 

        public string GetBaseUrl()
        {
            
            var request = HttpContext.Request.Path.ToString();
            //var urlHelper = new UrlHelper(request.ToString());
            //var baseUrl = $"{request.Url.Scheme}://{request.Url.Authority}{urlHelper.Content("~")}";
            return request;
        }

        public int GetUserId()
        {
            var result = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());

            return result;
        }
       
    }
}