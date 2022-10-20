using HumanResourceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanResourceManagementSystem.Controllers
{
    [AllowAnonymous]
    
    public class HomeController : Controller
    {
        HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();

        public ActionResult Index()
        {
            ViewBag.AllEmployee=db.Employee.Count();
            ViewBag.AllEvents = db.Event.Count();
            ViewBag.AllNotice = db.Notice.Count();
            TempData["name"] = "AmanUllah";
            TempData["institute"] = "ALFA ORIGEN";
            return View();
        }

        public ActionResult About()
        {
            TempData["name"] = "AmanUllah";

            return View();
        }

        public ActionResult Contact()
        {
            TempData["name"] = "AmanUllah";

            return View();
        }
        public ActionResult Services()
        {
            TempData["name"] = "AmanUllah";
            return View();
        }
       
        public ActionResult Project()
        {
            TempData["name"] = "AmanUllah";
            return View();
        }
        
    }
}