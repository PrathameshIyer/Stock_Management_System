using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MYBUSINESS.Models;

namespace MYBUSINESS.Controllers
{
    public class MyBusinessInfoesController : Controller
    {
        private BusinessContext db = new BusinessContext();

        // GET: MyBusinessInfoes
        public ActionResult Index()
        {
            return View(db.MyBusinessInfoes.ToList());
        }

        // GET: MyBusinessInfoes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyBusinessInfo myBusinessInfo = db.MyBusinessInfoes.Find(id);
            if (myBusinessInfo == null)
            {
                return HttpNotFound();
            }
            return View(myBusinessInfo);
        }

        // GET: MyBusinessInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Mobile,Email,Website,Tagline")] MyBusinessInfo myBusinessInfo)
        {
            if (ModelState.IsValid)
            {
                db.MyBusinessInfoes.Add(myBusinessInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myBusinessInfo);
        }

        // GET: MyBusinessInfoes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //MyBusinessInfo myBusinessInfo = db.MyBusinessInfoes.Find(id);
            MyBusinessInfo myBusinessInfo = db.MyBusinessInfoes.FirstOrDefault();
            if (myBusinessInfo == null)
            {
                return HttpNotFound();
            }
            return View(myBusinessInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Mobile,Email,Website,Tagline")] MyBusinessInfo myBusinessInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myBusinessInfo).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Create","SOSR");
                return RedirectToAction("Create", "SOSR", new { IsReturn = "false" });
            }
            return View(myBusinessInfo);
        }

        // GET: MyBusinessInfoes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyBusinessInfo myBusinessInfo = db.MyBusinessInfoes.Find(id);
            if (myBusinessInfo == null)
            {
                return HttpNotFound();
            }
            return View(myBusinessInfo);
        }

        // POST: MyBusinessInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MyBusinessInfo myBusinessInfo = db.MyBusinessInfoes.Find(id);
            db.MyBusinessInfoes.Remove(myBusinessInfo);
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
