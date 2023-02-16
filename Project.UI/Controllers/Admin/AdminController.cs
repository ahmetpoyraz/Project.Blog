using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
