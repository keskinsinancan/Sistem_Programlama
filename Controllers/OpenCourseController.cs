using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysProgAnket3.Models;

namespace SysProgAnket3.Controllers
{
    [Authorize(Roles = "admin, student, lecturer")]
    public class OpenCourseController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: OpenCourse
        public ActionResult Index()
        {
            var dersler = db.Dersler.Include(d => d.Ders_Detaylari).Where(d => d.Ders_Detaylari.acan_bolum_kodu == 11).Include(d => d.Hocalar);
            return View(dersler.ToList());
        }

        // GET: OpenCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // POST: OpenCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dersler dersler = db.Dersler.Find(id);
            db.Dersler.Remove(dersler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
