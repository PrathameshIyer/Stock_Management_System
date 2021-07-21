using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using MYBUSINESS.CustomClasses;
using System.Web.Routing;
using MYBUSINESS.Models;

namespace MYBUSINESS.Controllers
{
    public class DashboardController : Controller
    {
        private BusinessContext db = new BusinessContext();
 

        // GET: Dashboard
        public ActionResult Index()
        {
            decimal SaleOrderCount  = db.SOes.Count();
            ViewBag.SOCount = SaleOrderCount;

            decimal SaleOrderAmount = 0;
           SaleOrderAmount = (decimal)(db.SOes.Sum(x => x.SaleOrderAmount) ?? 0);
            //ViewBag.Sales = SaleOrderAmount;
            ViewBag.SOAmount = SaleOrderAmount;

            decimal PurchaseOrderCount = db.POes.Count();
            ViewBag.POCount = PurchaseOrderCount;

             decimal PurchaseOrderAmount = 0;

            PurchaseOrderAmount = (decimal)(db.POes.Sum(x => x.PurchaseOrderAmount) ?? 0);
             ViewBag.POAmount = PurchaseOrderAmount;

            decimal Profit = (decimal)(db.SOes.Sum(x => x.Profit) ?? 0);
            //ViewBag.Profit = (decimal)(SaleOrderCount - PurchaseOrderCount);

            ViewBag.Profit = Profit;


            ViewBag.Products = db.Products.Count();
            
            ViewBag.Suppliers = db.Suppliers.Count();
            
            ViewBag.Customers = db.Customers.Count();
            
            ViewBag.Employees = db.Employees.Count();

            return View();
        }
    }
}