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
    public class EmployeesManagmentController : Controller
    {
        private HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        // GET: EmployeesManagment
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                var employee = db.Employee.Include(e => e.Department);
                return View(employee.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and  Try Again");
            }
        }
        [HttpGet]
        public ActionResult List()
        {
            var list = db.Employee.ToList();

            return View(list);
        }

        //printing and filter
        public JsonResult Getdata(string data) {
            var list = db.Employee.ToList();
            return Json(list);
        }

        // GET: Email/Exist
        public JsonResult Checkexist(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            Employee SearchData = db.Employee.Where(x => x.Password == userdata).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        
        public JsonResult Exsitinput(string mobiledata)
        {
            System.Threading.Thread.Sleep(200);
            Employee SearchData = db.Employee.Where(x => x.Mobile == mobiledata).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        // GET: EmployeesManagment/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employee.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // GET: EmployeesManagment/Create
        public ActionResult Create()
        {
            Employee emp = new Employee();
            if (Session["AdminId"] != null)
            {
                ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName");
                var LastEmployee = db.Employee.OrderByDescending(x => x.EmployeeId).FirstOrDefault();
                if (LastEmployee == null)
                {
                    emp.username = "empid@1";
                }
                else
                {
                    emp.username = "empid@" + (LastEmployee.EmployeeId + 1).ToString();
                }

                return View(emp);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,DateOFBirth,Gender,Address,Mobile,DepartmentId,username,Password,EmpSalary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: EmployeesManagment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employee.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", employee.DepartmentId);
                return View(employee);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,DateOFBirth,Gender,Address,Mobile,DepartmentId,username,Password,EmpSalary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: EmployeesManagment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employee.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        // POST: EmployeesManagment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
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
