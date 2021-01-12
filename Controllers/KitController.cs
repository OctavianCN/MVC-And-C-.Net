using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_ShirtStore.Models;

namespace T_ShirtStore.Controllers
{
    public class KitController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Kit
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Kit> kits = db.Kits.ToList();
            ViewBag.Kits = kits;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if(id.HasValue)
            {
                Kit kit = db.Kits.Find(id);
                if(kit != null)
                {
                    return View(kit);
                }
                return HttpNotFound("Couldn't find the kit with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing book id parameter!");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            Kit kit = new Kit();
            kit.SaleList = GetAllSales();
            kit.SeasonList = GetAllSeasons();
            kit.TeamList = GetAllTeams();
            return View(kit);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult New(Kit kitRequest)
        {
            kitRequest.SaleList = GetAllSales();
            kitRequest.SeasonList = GetAllSeasons();
            kitRequest.TeamList = GetAllTeams();
            
            if(ModelState.IsValid)
            {
                db.Kits.Add(kitRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kitRequest);
            
        }
        [HttpGet]
        [Authorize(Roles = "Admin" + "," + "Helper")]
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Kit kit = db.Kits.Find(id);
                kit.SaleList = GetAllSales();
                kit.SeasonList = GetAllSeasons();
                kit.TeamList = GetAllTeams();
                if(kit == null)
                {
                    return HttpNotFound("Couldn't find the kit with id " + id.ToString() + "!");

                }
                return View(kit);
            }
            return HttpNotFound("Missing kit id parameter!");
        }
        [HttpPost]
        [Authorize(Roles = "Admin" + "," + "Helper")]
        public ActionResult Edit(int id,Kit kitRequest)
        {
            kitRequest.SaleList = GetAllSales();
            kitRequest.SeasonList = GetAllSeasons();
            kitRequest.TeamList = GetAllTeams();
            Kit kit = db.Kits.SingleOrDefault(b => b.KitId.Equals(id));
            if(ModelState.IsValid)
            {
                if(TryUpdateModel(kit))
                {
                    kit.KitName = kitRequest.KitName;
                    kit.SaleId = kitRequest.SaleId;
                    kit.SeasonId = kitRequest.SeasonId;
                    kit.TeamId = kitRequest.TeamId;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(kitRequest);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Kit kit = db.Kits.Find(id);
            if (kit != null)
            {
                db.Kits.Remove(kit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the kit with id " + id.ToString() + "!");
        }
        [NonAction]
        public IEnumerable<SelectListItem> GetAllSales()
        {
            var selectList = new List<SelectListItem>();
            foreach (var sale in db.Sales.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = sale.SaleId.ToString(),
                    Text = sale.Percent.ToString()
                });
            }
            return selectList;
        }
        [NonAction]
        public IEnumerable<SelectListItem> GetAllSeasons()
        {
            var selectList = new List<SelectListItem>();
            foreach (var season in db.Seasons.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = season.SeasonId.ToString(),
                    Text = season.SeasonName
                });
            }
            return selectList;
        }
        [NonAction]
        public IEnumerable<SelectListItem> GetAllTeams()
        {
            var selectList = new List<SelectListItem>();
            foreach (var team in db.Teams.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = team.TeamId.ToString(),
                    Text = team.TeamName
                });
            }
            return selectList;
        }
    }
}