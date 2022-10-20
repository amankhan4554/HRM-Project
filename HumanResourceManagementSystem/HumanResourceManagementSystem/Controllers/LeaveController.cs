using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumanResourceManagementSystem.Models;
using Newtonsoft.Json;

namespace HumanResourceManagementSystem.Controllers
{
    public class LeaveController : Controller
    {
        private HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        // GET: Leave
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {

                var leave = db.Leave.Include(l => l.Employee);
                return View(leave.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }
        [HttpGet]
        public ActionResult List()
        {
            var list = db.Leave.ToList();

            return View(list);
        }

        //printing and filter
        public JsonResult Getdata(string data)
        {
            var list = db.Leave.ToList();
            return Json(list);
        }
        public JsonResult existLeave(long userdata)
        {
            System.Threading.Thread.Sleep(200);
            Leave SearchData = db.Leave.Where(x => (x.Date.Value.Day == DateTime.Now.Day) || (x.EmployeeId == userdata)).SingleOrDefault();
            //Leave SearchData = db.Leave.Where(x => x.EmployeeId == userdata).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // GET: Leave/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Leave leave = db.Leave.Find(id);
                if (leave == null)
                {
                    return HttpNotFound();
                }
                return View(leave);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // GET: Leave/Create
        public ActionResult Create()
        {
                ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username");
                return View();
        }

        // POST: Leave/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveId,Subject,Date,Description,Status,EmployeeId")] Leave leave )
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (leave.Date.Value.Day == DateTime.Now.Day) return HttpNotFound("Too late or too early");
            var hasAttendance = db.Leave.Where(i => i.EmployeeId == leave.EmployeeId && i.Date.Value.Day == leave.Date.Value.Day&& i.Date.Value.Month == leave.Date.Value.Month && i.Date.Value.Year == leave.Date.Value.Year).FirstOrDefault();
            if (hasAttendance!=null)
            {
                ViewBag.Massage = "you are already Apply for Leave, You cannot Apply More";
            }
            else
            {
                try
                {
                    db.Leave.Add(leave);
                    db.SaveChanges();
                    return RedirectToAction("LeaveInformation");
                }
                catch (Exception ex)
                {

                    return HttpNotFound(ex.Message);
                }
            }
#pragma warning disable CS0162 // Unreachable code detected
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", leave.EmployeeId);
#pragma warning restore CS0162 // Unreachable code detected
            return View(leave);
            
        }

        // GET: Leave/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Leave leave = db.Leave.Find(id);
                if (leave == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", leave.EmployeeId);
                return View(leave);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Leave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveId,Subject,Date,Description,Status,EmployeeId")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leave).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", leave.EmployeeId);
            return View(leave);
        }

        // GET: Leave/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Leave leave = db.Leave.Find(id);
                if (leave == null)
                {
                    return HttpNotFound();
                }
                return View(leave);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Leave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leave leave = db.Leave.Find(id);
            db.Leave.Remove(leave);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LeaveInformation()
        {
            if (Session["UserId"] != null)
            {
                long empid = (long)Session["UniqueId"];
                return View(db.Leave.Where(x => x.EmployeeId == empid).ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
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
