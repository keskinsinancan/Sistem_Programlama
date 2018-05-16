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
    [Authorize(Roles = "Admin")]
    public class DersDetaylariController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: DersDetaylari
        public ActionResult Index()
        {
            var ders_Detaylari = db.Ders_Detaylari.Include(d => d.Bolumler);
            var model = new List<Ders_Detaylari>();
            foreach(var dt in ders_Detaylari)
            {
                if(dt.acan_bolum_kodu == 11)
                {
                    model.Add(dt);
                }
            }
            return View(model);
        }

        // GET: DersDetaylari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders_Detaylari ders_Detaylari = db.Ders_Detaylari.Find(id);
            if (ders_Detaylari == null)
            {
                return HttpNotFound();
            }
            return View(ders_Detaylari);
        }

        // GET: DersDetaylari/Create
        public ActionResult Create()
        {
            ViewBag.acan_bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi");
            return View();
        }

        // POST: DersDetaylari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ders_kodu,ders_adi,teori,pratik,lab,toplam_kredi,acan_bolum_kodu")] Ders_Detaylari ders_Detaylari)
        {
            if (ModelState.IsValid)
            {
                db.Ders_Detaylari.Add(ders_Detaylari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.acan_bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", ders_Detaylari.acan_bolum_kodu);
            return View(ders_Detaylari);
        }

        // GET: DersDetaylari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders_Detaylari ders_Detaylari = db.Ders_Detaylari.Find(id);
            if (ders_Detaylari == null)
            {
                return HttpNotFound();
            }
            ViewBag.acan_bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", ders_Detaylari.acan_bolum_kodu);
            return View(ders_Detaylari);
        }

        // POST: DersDetaylari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ders_kodu,ders_adi,teori,pratik,lab,toplam_kredi,acan_bolum_kodu")] Ders_Detaylari ders_Detaylari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ders_Detaylari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.acan_bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", ders_Detaylari.acan_bolum_kodu);
            return View(ders_Detaylari);
        }

        // GET: DersDetaylari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders_Detaylari ders_Detaylari = db.Ders_Detaylari.Find(id);
            if (ders_Detaylari == null)
            {
                return HttpNotFound();
            }
            return View(ders_Detaylari);
        }

        // POST: DersDetaylari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ders_Detaylari ders_Detaylari = db.Ders_Detaylari.Find(id);
            db.Ders_Detaylari.Remove(ders_Detaylari);
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
