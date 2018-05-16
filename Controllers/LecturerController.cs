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
    public class LecturerController : Controller
    {
        private SysProgDbEntities db = new SysProgDbEntities();

        // GET: Lecturer
        public ActionResult Index()
        {
            var hocalar = db.Hocalar.Include(h => h.Bolumler);
            var model = new List<Hocalar>();
            foreach(var lec in hocalar)
            {
                if(lec.bolum_kodu == 11)
                {
                    model.Add(lec);
                }
            }
            return View(model);
        }

        // GET: Lecturer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hocalar hocalar = db.Hocalar.Find(id);
            if (hocalar == null)
            {
                return HttpNotFound();
            }
            return View(hocalar);
        }

        // GET: Lecturer/Create
        public ActionResult Create()
        {
            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi");
            return View();
        }

        // POST: Lecturer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hoca_kodu,adi,soyadi,bolum_kodu")] Hocalar hocalar)
        {
            if (ModelState.IsValid)
            {
                db.Hocalar.Add(hocalar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", hocalar.bolum_kodu);
            return View(hocalar);
        }

        // GET: Lecturer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hocalar hocalar = db.Hocalar.Find(id);
            if (hocalar == null)
            {
                return HttpNotFound();
            }
            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", hocalar.bolum_kodu);
            return View(hocalar);
        }

        // POST: Lecturer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hoca_kodu,adi,soyadi,bolum_kodu")] Hocalar hocalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bolum_kodu = new SelectList(db.Bolumler, "bolum_kodu", "bolum_adi", hocalar.bolum_kodu);
            return View(hocalar);
        }

        // GET: Lecturer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hocalar hocalar = db.Hocalar.Find(id);
            if (hocalar == null)
            {
                return HttpNotFound();
            }
            return View(hocalar);
        }

        // POST: Lecturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hocalar hocalar = db.Hocalar.Find(id);
            db.Hocalar.Remove(hocalar);
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
