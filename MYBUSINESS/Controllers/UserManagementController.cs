using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MYBUSINESS.CustomClasses;
using MYBUSINESS.Models;

namespace MYBUSINESS.Controllers
{
    public class UserManagementController : Controller
    {
        private BusinessContext db = new BusinessContext();
        public ActionResult Login()
        {
            
            Session.Clear();
            Session.Abandon();
               return View();
        }
       
        public ActionResult Logout()
        {
            
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
            

        }

        [HttpPost]
        public ActionResult Login(Employee emp)
        //public ActionResult Login(Employee emp, Right right)
        {
            
            
            
           if (emp.Password == null) { emp.Password = string.Empty; }
            emp.Password = Encryption.Encrypt(emp.Password, "d3A#");

            //it sounds singelordefault will give error if value is more than one.
            MYBUSINESS.Models.Employee user = db.Employees.SingleOrDefault(usr => ((usr.Login == emp.Login) && (usr.Password == emp.Password)));
            if (user != null)
            {
               
                Session.Add("CurrentUser", user);
                return RedirectToAction("Index", "Dashboard");//change it from 'if condtion' to here
                //return View("Index", "DashBoard",user);
            }
            else
            {
                TempData["message"] = "Password is not valid";
                return RedirectToAction("Login", "UserManagement");
            }
            
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /UserManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
