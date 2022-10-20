using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumanResourceManagementSystem.Models;

namespace HumanResourceManagementSystem.Controllers
{
    public class NoticeController : Controller
    {
        private HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        // GET: Notice
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.Notice.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }
        // GET: Notice/Exist
        public JsonResult exitInput(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            Notice SearchData = db.Notice.Where(x => x.Subject == userdata).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        // GET: Notice/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notice notice = db.Notice.Find(id);
                if (notice == null)
                {
                    return HttpNotFound();
                }
                return View(notice);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // GET: Notice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,Date,Time,Description")] Notice notice)
        {
            if (ModelState.IsValid)
            {
                db.Notice.Add(notice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notice);
        }

        // GET: Notice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notice notice = db.Notice.Find(id);
                if (notice == null)
                {
                    return HttpNotFound();
                }
                return View(notice);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Notice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,Date,Time,Description")] Notice notice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notice).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notice);
        }

        // GET: Notice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notice notice = db.Notice.Find(id);
                if (notice == null)
                {
                    return HttpNotFound();
                }
                return View(notice);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Notice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notice notice = db.Notice.Find(id);
            db.Notice.Remove(notice);
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
