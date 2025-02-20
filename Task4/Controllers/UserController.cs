using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
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
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
            {
                TempData["MSG"] = "All fields are required!";
                return RedirectToAction("Register");
            }

            if (password != repeatPassword)
            {
                TempData["MSG"] = "Passwords do not match!";
                return RedirectToAction("Register");
            }

            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Password", password);

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            var data = Request.Cookies["userInfo"];
            if (data != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult HandleLogin(string email, string password, string rem)
        {
            var storedEmail = HttpContext.Session.GetString("Email");
            var storedPassword = HttpContext.Session.GetString("Password");

            if ((email == storedEmail && password == storedPassword) || (email == "admin@gmail.com" && password == "111"))
            {
                if (rem != null)
                {
                    CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(7) };
                    Response.Cookies.Append("userInfo", email, options);
                }

                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("Password", password);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid email or password!";
                return View("Login");
            }
        }

        
        public IActionResult Profile()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Password = HttpContext.Session.GetString("Password");  

            TempData["Name"] = ViewBag.Name;

            return View();
        }


    }
}
