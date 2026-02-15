using Microsoft.AspNetCore.Mvc;

namespace SAFLC_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // TESTING: Change this to "Teacher" to see the other UI
            string role = "Admin";

            ViewBag.Role = role;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
