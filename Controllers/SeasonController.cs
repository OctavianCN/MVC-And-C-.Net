using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_ShirtStore.Models;

namespace T_ShirtStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SeasonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Season
        public ActionResult Index()
        {
            ViewBag.Seasons = db.Seasons.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult New()
        {
            Season season = new Season();
            return View(season);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Season season = db.Seasons.Find(id);
                if (season != null)
                {
                    return View(season);
                }
                return HttpNotFound("Couldn't find the season with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing season id parameter!");
        }
        [HttpPost]
        public ActionResult New(Season seasonRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Seasons.Add(seasonRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(seasonRequest);
            }
            catch (Exception e)
            {
                return View(seasonRequest);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Season season = db.Seasons.Find(id);
                if (season == null)
                {
                    return HttpNotFound("Couldn't find the sale wit id " + id.ToString() + "!");
                }
                return View(season);
            }
            return HttpNotFound("Couldn't find the sale wit id " + id.ToString() + "!");
        }
        [HttpPost]
        public ActionResult Edit(int id, Season seasonRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Season season = db.Seasons.Find(id);
                    if (TryUpdateModel(season))
                    {
                        season.SeasonName = seasonRequest.SeasonName;
                        season.Price = seasonRequest.Price;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(seasonRequest);
            }
            catch (Exception e)
            {
                return View(seasonRequest);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Season season = db.Seasons.Find(id);
                if (season != null)
                {
                    db.Seasons.Remove(season);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find a season with id " + id.ToString() + "!");
            }
            return HttpNotFound("Season id parameter is missing!");
        }
    }
}