using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class OrnekController : Controller
    {

        public IActionResult Index()
        {
            /*
            ViewBag.name = "Asp.Net Core";
            ViewData["age"] = 24;
            ViewData["names"] = new List<string>() { "Tunahan", "Ertuğrul", "Lütfücan" };
            ViewBag.person = new { Id = 1, name = "Tunahan", age = 24 };
            */
            /*
            ViewBag.name = "Tunahan";
            TempData["surname"] = "Kılıç";
            */
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return RedirectToAction("Index", "Ornek");
        }


        public IActionResult ContentResult()
        {
            return Content("Content Result");
        }
        public IActionResult JsonResult()
        {
            return Json(new { Id = 1, name = "Kalem 1", price = 100 });
        }

        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }
    }
}
