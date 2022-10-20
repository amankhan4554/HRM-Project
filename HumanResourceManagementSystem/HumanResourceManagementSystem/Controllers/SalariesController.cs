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
    public class SalariesController : Controller
    {
        private HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        // GET: Salaries
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                var salary = db.Salary.Include(s => s.Employee);
                return View(salary.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            var list = db.Salary.ToList();

            return View(list);
        }

        //printing and filter
        public JsonResult Getdata(string data)
        {
            var list = db.Salary.ToList();
            return Json(list);
        }
        // GET: Salaries/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Salary salary = db.Salary.Find(id);
                if (salary == null)
                {
                    return HttpNotFound();
                }
                return View(salary);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // GET: Salaries/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username");
                ViewBag.TotalSalary = new SelectList(db.Employee, "EmployeeId", "EmpSalary");
                return View();
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Salaries/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryId,Date,TotalSalary,UserName,RemaningSalary,EmployeeId,UserName")] Salary salary)
        {
            var hasAttendance = db.Salary.Where(i => i.EmployeeId == salary.EmployeeId && i.Date.Value.Month == salary.Date.Value.Month && i.Date.Value.Year == salary.Date.Value.Year).FirstOrDefault();
            if (hasAttendance != null)
            {
                ViewBag.Massage = "your Salary Already Done, You cannot Add More";
            }
            else
            {
                try
                {
                    db.Salary.Add(salary);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    return HttpNotFound(ex.Message);
                }
            }
#pragma warning disable CS0162 // Unreachable code detected
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", salary.EmployeeId);
#pragma warning restore CS0162 // Unreachable code detected
            ViewBag.TotalSalary = new SelectList(db.Employee, "EmployeeId", "EmpSalary", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Salary salary = db.Salary.Find(id);
                if (salary == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", salary.EmployeeId);
                ViewBag.TotalSalary = new SelectList(db.Employee, "EmployeeId", "EmpSalary", salary.EmployeeId);
                return View(salary);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryId,Date,TotalSalary,RemaningSalary,EmployeeId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "username", salary.EmployeeId);
            ViewBag.TotalSalary = new SelectList(db.Employee, "EmployeeId", "EmpSalary", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Salary salary = db.Salary.Find(id);
                if (salary == null)
                {
                    return HttpNotFound();
                }
                return View(salary);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = db.Salary.Find(id);
            db.Salary.Remove(salary);
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
