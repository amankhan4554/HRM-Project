using HumanResourceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourceManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();
        // GET: Employee
        public ActionResult Dashboard()
        {
            if (Session["UserId"] != null)
            {
                TempData["Employee"] = User.Identity.Name;
                ViewBag.AllEmployees = db.Employee.Count();
                ViewBag.AllDepartments = db.Department.Count();
                ViewBag.AllEvents = db.Event.Count();
                ViewBag.AllNotice = db.Notice.Count();
                return View();
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
            
        }
        public ActionResult SalaryInformation()
        {
            if (Session["UserId"] != null)
            {
               long empid = (long)Session["UniqueId"];
                return View(db.Salary.Where(x => x.EmployeeId == empid).ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }
        public ActionResult AttendanceInformation()
        {
            if (Session["UserId"] != null)
            {
                long empid = (long)Session["UniqueId"];
                return View(db.Attendance.Where(x => x.EmployeeId == empid).ToList());
            }
            else
            {
                return HttpNotFound();
            }

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
        public ActionResult EventInformation()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Event.ToList());
            }else { return HttpNotFound("Please Login Your Account and Try Again"); }
        }
        public ActionResult NoticeInformation()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Notice.ToList());
            }else { return HttpNotFound("Please Login Your Account and Try Again");
            }
        }

        public ActionResult EmployeeInformation()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Employee.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }

            
        }
        public ActionResult DepartmentInformation()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Department.ToList());
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
        }
    }
}