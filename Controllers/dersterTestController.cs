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
    public class dersterTestController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: dersterTest
        public ActionResult Index()
        {
            var dersler = db.Dersler.Include(d => d.Ders_Detaylari).Include(d => d.Hocalar);
            return View(dersler.ToList());
        }

        // GET: dersterTest/Details/5
        public ActionResult Details(int? id)
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

        // GET: dersterTest/Create
        public ActionResult Create()
        {
            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi");
            ViewBag.hoca_kodu = new SelectList(db.Hocalar, "hoca_kodu", "adi");
            return View();
        }

        // POST: dersterTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ders_kodu,grup_no,dersin_adi,hoca_kodu")] Dersler dersler)
        {
            if (ModelState.IsValid)
            {
                db.Dersler.Add(dersler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", dersler.ders_kodu);
            ViewBag.hoca_kodu = new SelectList(db.Hocalar, "hoca_kodu", "adi", dersler.hoca_kodu);
            return View(dersler);
        }

        // GET: dersterTest/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", dersler.ders_kodu);
            ViewBag.hoca_kodu = new SelectList(db.Hocalar, "hoca_kodu", "adi", dersler.hoca_kodu);
            return View(dersler);
        }

        // POST: dersterTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ders_kodu,grup_no,dersin_adi,hoca_kodu")] Dersler dersler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dersler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ders_kodu = new SelectList(db.Ders_Detaylari, "ders_kodu", "ders_adi", dersler.ders_kodu);
            ViewBag.hoca_kodu = new SelectList(db.Hocalar, "hoca_kodu", "adi", dersler.hoca_kodu);
            return View(dersler);
        }

        // GET: dersterTest/Delete/5
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

        // POST: dersterTest/Delete/5
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
