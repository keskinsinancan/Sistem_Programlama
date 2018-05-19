using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysProgAnket3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (HttpContext.User != null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.message = "Hatalı giriş";
                return RedirectToAction("Login", "Account");
            }
        }
    }
}