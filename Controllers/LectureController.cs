using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SysProgAnket3.Models;

namespace SysProgAnket3.Controllers
{
    
    public class LectureController : Controller
    {
        // GET: Lecture
        private readonly SysProgDbEntities db = new SysProgDbEntities();
        [Authorize(Roles = "student,admin,lecturer")]
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        [Authorize(Roles = "student,admin")]
        public ActionResult AlinanDersler()
        {   
            var stdId = Regex.Match(this.User.Identity.Name, @"\d+").Value;
            int _stdId = Convert.ToInt32(stdId);
            var model = (db.Ogrenci_Ders.Where(m => m.ogr_no == _stdId)).ToList();
            return View("~/Views/Lecture/Index.cshtml", model);
        }
        [Authorize(Roles = "admin,lecturer")]
        public ActionResult VerilenDersler()
        {
            var hocaKodu = Regex.Match(this.User.Identity.Name, @"\d+").Value;
            int hocaId = Convert.ToInt32(hocaKodu);
            var model = (db.Dersler.Where(m => m.hoca_kodu == hocaId)).ToList();
            return View("~/Views/Lecture/HocaDers.cshtml", model);
        }
    }
}