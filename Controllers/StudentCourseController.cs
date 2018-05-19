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
    public class StudentCourseController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: StudentCourse
        public ActionResult Index()
        {
            var ogrenci_Ders = db.Ogrenci_Ders.Include(o => o.Ders_Detaylari).Include(o => o.Ogrenciler);
            return View(ogrenci_Ders.ToList());
        }

        // GET: StudentCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci_Ders ogrenci_Ders = db.Ogrenci_Ders.Find(id);
            if (ogrenci_Ders == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci_Ders);
        }

        // GET: StudentCourse/Create
        public ActionResult Create()
        {
            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari.Where(dd => dd.acan_bolum_kodu == 11), "ders_kodu", "ders_adi");
            ViewBag.ogr_no = new SelectList(db.Ogrenciler.Where(o => o.bolum_kodu == 11), "ogr_no", "ogr_no");
            return View();
        }

        // POST: StudentCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ogr_no,ders_kodu,grubu,ders_adi")] Ogrenci_Ders ogrenci_Ders)
        {
            var ders = db.Ders_Detaylari.Find(ogrenci_Ders.ders_kodu);
            ogrenci_Ders.ders_adi = ders.ders_adi;

            var isTaken = db.Ogrenci_Ders.Where(od => od.ders_kodu == ogrenci_Ders.ders_kodu && od.ogr_no == ogrenci_Ders.ogr_no);
            if (isTaken == null)
            {
                if (ModelState.IsValid)
                {
                    db.Ogrenci_Ders.Add(ogrenci_Ders);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            else
            {
                ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", ogrenci_Ders.ders_kodu);
                ViewBag.ogr_no = new SelectList(db.Ogrenciler, "ogr_no", "adi", ogrenci_Ders.ogr_no);
                ViewBag.message = "Öğrenci dersi zaten almış.";
                return View();
            }

            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", ogrenci_Ders.ders_kodu);
            ViewBag.ogr_no = new SelectList(db.Ogrenciler, "ogr_no", "adi", ogrenci_Ders.ogr_no);
            return View(ogrenci_Ders);
        }

        // GET: StudentCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci_Ders ogrenci_Ders = db.Ogrenci_Ders.Find(id);
            if (ogrenci_Ders == null)
            {
                return HttpNotFound();
            }
            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", ogrenci_Ders.ders_kodu);
            ViewBag.ogr_no = new SelectList(db.Ogrenciler, "ogr_no", "adi", ogrenci_Ders.ogr_no);
            return View(ogrenci_Ders);
        }

        // POST: StudentCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ogr_no,ders_kodu,grubu,ders_adi")] Ogrenci_Ders ogrenci_Ders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenci_Ders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", ogrenci_Ders.ders_kodu);
            ViewBag.ogr_no = new SelectList(db.Ogrenciler, "ogr_no", "adi", ogrenci_Ders.ogr_no);
            return View(ogrenci_Ders);
        }

        // GET: StudentCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci_Ders ogrenci_Ders = db.Ogrenci_Ders.Find(id);
            if (ogrenci_Ders == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci_Ders);
        }

        // POST: StudentCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ogrenci_Ders ogrenci_Ders = db.Ogrenci_Ders.Find(id);
            db.Ogrenci_Ders.Remove(ogrenci_Ders);
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
