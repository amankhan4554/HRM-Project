using HumanResourceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HumanResourceManagementSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        HumanResourceManagementSystemEntities db = new HumanResourceManagementSystemEntities();
        // GET: User


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin UserData)
        {
            using (var context = new HumanResourceManagementSystemEntities())
            {
                // bool isValid = context.Admin.Any(x => x.Email == UserData.Email && x.Password == UserData.Password);
                var user = context.Admin.FirstOrDefault(x => x.Email == UserData.Email && x.Password == UserData.Password);
                if (user != null)
                {
                    Session["AdminId"] = user.AdminId;
                    FormsAuthentication.SetAuthCookie(UserData.Email, false);
                    return RedirectToAction("Dashboard", "Admin");
                }
                ModelState.AddModelError("", "Invalid email and Password");
                return View();
            }

        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SignIn(Employee UserData)
        {
            using (var context = new HumanResourceManagementSystemEntities())
            {
                // bool isValid = context.Admin.Any(x => x.Email == UserData.Email && x.Password == UserData.Password);
                var user = context.Employee.FirstOrDefault(x => x.username == UserData.username && x.Password == UserData.Password);//&& x.Password == UserData.Password);
                if (user != null)
                {
                    Session["UserId"] = user.EmployeeId;
                   Session["UniqueId"] = user.EmployeeId;
                   FormsAuthentication.SetAuthCookie(UserData.username, false);
                    return RedirectToAction("Dashboard", "Employee");
                }
                ModelState.AddModelError("", "Invalid email and Employee ID");
                return View();
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}