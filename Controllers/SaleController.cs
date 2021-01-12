using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_ShirtStore.Models;

namespace T_ShirtStore.Controllers
{
    [Authorize(Roles = "Admin" + "," + "Helper")]
    public class SaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sale
        public ActionResult Index()
        {
            ViewBag.Sales = db.Sales.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Sale sale = db.Sales.Find(id);
                if (sale != null)
                {
                    return View(sale);
                }
                return HttpNotFound("Couldn't find the sale with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing sale id parameter!");
        }
        [HttpGet]
        public ActionResult New()
        {
            Sale sale = new Sale();
            return View(sale);
        }
        [HttpPost]
        public ActionResult New(Sale saleRequest)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Sales.Add(saleRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(saleRequest);
            }
            catch(Exception e)
            {
                return View(saleRequest);
            }
        }
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Sale sale = db.Sales.Find(id);
                if(sale == null)
                {
                    return HttpNotFound("Couldn't find the sale wit id " + id.ToString() + "!");
                }
                return View(sale);
            }
            return HttpNotFound("Couldn't find the sale wit id " + id.ToString() + "!");
        }
        [HttpPost]
        public ActionResult Edit(int id,Sale saleRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Sale sale = db.Sales.Find(id);
                    if (TryUpdateModel(sale))
                    {
                        sale.Percent = saleRequest.Percent;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(saleRequest);
            }
            catch (Exception e)
            {
                return View(saleRequest);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if(id.HasValue)
            {
                Sale sale = db.Sales.Find(id);
                if(sale != null)
                {
                    db.Sales.Remove(sale);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the sale cupon with id " + id.ToString() + "!");
            }
            return HttpNotFound("Sale cupon id parameter is missing!");
        }
    }
}