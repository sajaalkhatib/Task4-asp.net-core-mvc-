using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("Name");

            if (!string.IsNullOrEmpty(userName))
            {
                ViewBag.WelcomeMessage = $"Welcome, {userName}!";
            }
            else
            {
                ViewBag.WelcomeMessage = "Welcome, Guest!";
            }

            return View();
        }
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Register", "User");
        }

    }
}
