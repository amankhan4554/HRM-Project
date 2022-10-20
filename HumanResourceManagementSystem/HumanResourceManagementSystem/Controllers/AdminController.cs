using HumanResourceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourceManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["AdminId"]!=null)
            {
                TempData["Admin"] = "AmanUllah";
                ViewBag.AllEmployee = db.Employee.Count();
                ViewBag.AllEvent = db.Event.Count();
                ViewBag.AllNotice = db.Notice.Count();
                ViewBag.AllDepartment = db.Department.Count();
                ViewBag.AllLeave = db.Leave.Count();
                return View();
            }
            else
            {
                return HttpNotFound("Please Login Your Account and Try Again");
            }
            
           
        }
    }
}