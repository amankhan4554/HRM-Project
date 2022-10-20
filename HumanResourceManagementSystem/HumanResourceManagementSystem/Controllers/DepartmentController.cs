using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumanResourceManagementSystem.Models;

namespace HumanResourceManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        // GET: Department
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.Department.ToList());
            }
            return HttpNotFound("Please Login Your Account and Try Again");

        }
        // GET: Department/Exist
        public JsonResult exitInput(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            Department SearchData = db.Department.Where(x => x.DepartmentName == userdata).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // GET: Department/Details/5
        
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Department/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName,DepartmentDescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Department.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Department/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName,DepartmentDescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Department.Find(id);
            db.Department.Remove(department);
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
