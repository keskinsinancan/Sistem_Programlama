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
    [Authorize(Roles = "admin")]
    public class StudentController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: Student
        public ActionResult Index()
        {
            var ogrenciler = db.Ogrenciler.Include(o => o.Bolumler);
            var model = new List<Ogrenciler>();
            foreach (var std in ogrenciler)
            {
                if (std.bolum_kodu== 11)
                {
                    model.Add(std);
                }
            }
            return View(model);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ogr_no,adi,soyadi,bolum_kodu")] Ogrenciler ogrenciler)
        {
            if (ModelState.IsValid)
            {
                db.Ogrenciler.Add(ogrenciler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", ogrenciler.bolum_kodu);
            return View(ogrenciler);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", ogrenciler.bolum_kodu);
            return View(ogrenciler);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ogr_no,adi,soyadi,bolum_kodu")] Ogrenciler ogrenciler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenciler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", ogrenciler.bolum_kodu);
            return View(ogrenciler);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            db.Ogrenciler.Remove(ogrenciler);
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
