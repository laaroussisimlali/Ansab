using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ansab.Models;

namespace Ansab.Controllers
{
    public class RelationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relations
        public ActionResult Index()
        {
            var relations = db.Relations.Include(r => r.Husband).Include(r => r.Wife);
            return View(relations.ToList());
        }

        // GET: Relations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                return HttpNotFound();
            }
            return View(relation);
        }

        // GET: Relations/Create
        public ActionResult Create()
        {
            ViewBag.HusbandId = new SelectList(db.People, "Id", "FirstName");
            ViewBag.WifeId = new SelectList(db.People, "Id", "FirstName");
            return View();
        }

        // POST: Relations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HusbandId,WifeId")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                db.Relations.Add(relation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HusbandId = new SelectList(db.People, "Id", "FirstName", relation.HusbandId);
            ViewBag.WifeId = new SelectList(db.People, "Id", "FirstName", relation.WifeId);
            return View(relation);
        }

        // GET: Relations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HusbandId = new SelectList(db.People, "Id", "FirstName", relation.HusbandId);
            ViewBag.WifeId = new SelectList(db.People, "Id", "FirstName", relation.WifeId);
            return View(relation);
        }

        // POST: Relations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HusbandId,WifeId")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HusbandId = new SelectList(db.People, "Id", "FirstName", relation.HusbandId);
            ViewBag.WifeId = new SelectList(db.People, "Id", "FirstName", relation.WifeId);
            return View(relation);
        }

        // GET: Relations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                return HttpNotFound();
            }
            return View(relation);
        }

        // POST: Relations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relation relation = db.Relations.Find(id);
            db.Relations.Remove(relation);
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
