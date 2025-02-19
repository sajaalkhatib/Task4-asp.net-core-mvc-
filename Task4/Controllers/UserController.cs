using Microsoft.AspNetCore.Mvc;

namespace Task4.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HandleRegister(string name, string email, string password, string repeatPassword)
        {

            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Password", password);
            HttpContext.Session.SetString("repeatPassword", repeatPassword);


            if (password != repeatPassword)
            {
                TempData["MSG"] = "Password doesn't match";
                return RedirectToAction("Register");

            }

            return RedirectToAction("Login");

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HandleLogin(string email, string password)
        {
            var storedEmail = HttpContext.Session.GetString("Email");
            var storedPassword = HttpContext.Session.GetString("Password");

            if (email == storedEmail && password == storedPassword)
            {
                return RedirectToAction("index", "Home");
            }

            TempData["MSG"] = "Invalid username or password.";
            return RedirectToAction("Login");
        }
        public IActionResult Profile()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Password = HttpContext.Session.GetString("Password");



            TempData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Name"] = HttpContext.Session.GetString("Name");



            return View();

        }
    }
}
