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
    public class OgrenciDersController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: OgrenciDers
        public ActionResult Index()
        {
            var ogrenci_Ders = db.Ogrenci_Ders.Include(o => o.Ders_Detaylari).Include(o => o.Ogrenciler);
            var model = new List<Ogrenci_Ders>();
            foreach(var drs in ogrenci_Ders)
            {
                if(drs.Ders_Detaylari.acan_bolum_kodu == 11)
                {
                    model.Add(drs);
                }
            }
            return View(model);
        }

        // GET: OgrenciDers/Details/5
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

        // GET: OgrenciDers/Create
        public ActionResult Create()
        {
            var dt = (db.Ders_Detaylari.Where(m => m.acan_bolum_kodu == 11));
            ViewBag.ders_kodu = new SelectList(dt, "ders_kodu", "ders_adi");
            var ogr = (db.Ogrenciler.Where(m => m.bolum_kodu == 11));
            ViewBag.ogr_no = new SelectList(ogr, "ogr_no", "ogr_no");
            return View();
        }

        // POST: OgrenciDers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ogr_no,ders_kodu,grubu,ders_adi")] Ogrenci_Ders ogrenci_Ders)
        {
            if (ModelState.IsValid)
            {
                db.Ogrenci_Ders.Add(ogrenci_Ders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", ogrenci_Ders.ders_kodu);
            ViewBag.ogr_no = new SelectList(db.Ogrenciler, "ogr_no", "adi", ogrenci_Ders.ogr_no);
            return View(ogrenci_Ders);
        }

        // GET: OgrenciDers/Edit/5
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

        // POST: OgrenciDers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ogr_no,ders_kodu,grubu,ders_adi")] Ogrenci_Ders ogrenci_Ders)
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

        // GET: OgrenciDers/Delete/5
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

        // POST: OgrenciDers/Delete/5
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
