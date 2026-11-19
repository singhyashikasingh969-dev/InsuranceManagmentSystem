using Microsoft.AspNetCore.Mvc;

namespace InsuranceManagmentSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }
    }
}