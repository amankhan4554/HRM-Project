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
    public class AttendanceController : Controller
    {
        private HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        // GET: Attendance
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                var attendance = db.Attendance.Include(a => a.Employee);
                return View(attendance.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }
        [HttpGet]
        public ActionResult List()
        {
            var list = db.Attendance.ToList();

            return View(list);
        }

        //printing and filter
        public JsonResult Getdata(string data)
        {
            var list = db.Attendance.ToList();
            return Json(list);
        }
        // GET: Attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] !=null)
            {
                if (id == null)
                {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
               Attendance attendance = db.Attendance.Find(id);
               if (attendance == null)
               {
                   return HttpNotFound();
               }
              
                   return View(attendance);

            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }
       
        // GET: Attendance/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username");
                return View();
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Attendance/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceId,Date,Status,WorkDetail,EmployeeId")] Attendance attendance )
        {

            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (leave.Date.Value.Day == DateTime.Now.Day) return HttpNotFound("Too late or too early");
            var hasAttendance = db.Attendance.Where(i => i.EmployeeId == attendance.EmployeeId && i.Date.Value.Day == attendance.Date.Value.Day && i.Date.Value.Month == attendance.Date.Value.Month && i.Date.Value.Year == attendance.Date.Value.Year).FirstOrDefault();
            if (hasAttendance != null)
            {
                ViewBag.Massage = "Your Attendance already done, You cannot add More";
                //return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
                //return HttpNotFound("Your Attendance already done");
            }
            else
            {
                try
                {
                    db.Attendance.Add(attendance);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    return HttpNotFound(ex.Message);
                }
            }
#pragma warning disable CS0162 // Unreachable code detected
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", attendance.EmployeeId);
#pragma warning restore CS0162 // Unreachable code detected
            return View(attendance);
            
        }

        // GET: Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {

               
               if (id == null)
               {
                   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               }
               Attendance attendance = db.Attendance.Find(id);
               if (attendance == null)
               {
                   return HttpNotFound();
               }
               ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", attendance.EmployeeId);
               return View(attendance);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Attendance/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceId, Date, Status, WorkDetail, EmployeeId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", attendance.EmployeeId);
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Attendance attendance = db.Attendance.Find(id);
                if (attendance == null)
                {
                    return HttpNotFound();
                }
                return View(attendance);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
            
            
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendance.Find(id);
            db.Attendance.Remove(attendance);
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
