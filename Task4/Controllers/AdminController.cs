using Microsoft.AspNetCore.Mvc;

namespace Task4.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
