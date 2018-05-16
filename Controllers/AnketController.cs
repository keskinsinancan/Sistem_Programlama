using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysProgAnket3.Controllers
{
    public class AnketController : Controller
    {
        // GET: Anket
        [Authorize(Roles = "student,admin,lecturer")]
        public ActionResult Index()
        {
            return View("~/Views/Anket/Index.cshtml");
        }
    }
}