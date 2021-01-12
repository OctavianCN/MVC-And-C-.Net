using lab3_miercuri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_ShirtStore.Models;

namespace T_ShirtStore.Controllers
{
    public class TeamController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Team
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Team> teams = db.Teams.ToList();
            ViewBag.Teams = teams;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Team team = db.Teams.Find(id);
                if (team != null)
                {
                    return View(team);
                }
                return HttpNotFound("Couldn't find the team with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing team id parameter!");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            Team team = new Team();
            team.PlayersList = GetAllPlayers();
            return View(team);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Team teamRequest)
        {
            if (teamRequest.PlayersList != null)
            {
                var selectedPlayers = teamRequest.PlayersList.Where(b => b.Checked).ToList();
                try
                {
                    if (ModelState.IsValid)
                    {
                        teamRequest.Players = new List<Player>();
                        for (int i = 0; i < selectedPlayers.Count(); i++)
                        {

                            Player player = db.Players.Find(selectedPlayers[i].Id);
                            teamRequest.Players.Add(player);
                        }
                        db.Teams.Add(teamRequest);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(teamRequest);
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                    return View(teamRequest);
                }
            }
            else
            {
                db.Teams.Add(teamRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamRequest);

        }
        [HttpGet]
        [Authorize(Roles = "Admin" + "," + "Helper")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Team team = db.Teams.Find(id);
                team.PlayersList = GetAllPlayers();
                foreach (Player checkedPlayer in team.Players)
                {
                    team.PlayersList.FirstOrDefault(g => g.Id == checkedPlayer.PlayerId).Checked = true;

                }
                if (team == null)
                {
                    return HttpNotFound("Coludn't find the team with id " + id.ToString() + "!");
                }
                return View(team);
            }
            return HttpNotFound("Missing team id parameter");
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin" + "," + "Helper")]
        public ActionResult Edit(int id, Team teamRequest)
        {
            Team team = db.Teams.SingleOrDefault(b => b.TeamId.Equals(id));
            var selectedPlayers = teamRequest.PlayersList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(team))
                    {
                        team.TeamName = teamRequest.TeamName;
                        team.Players.Clear();
                        team.Players = new List<Player>();
                        for (int i = 0; i < selectedPlayers.Count(); i++)
                        {
                            Player player = db.Players.Find(selectedPlayers[i].Id);
                            team.Players.Add(player);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(teamRequest);
            }
            catch (Exception)
            {
                return View(teamRequest);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Team team = db.Teams.Find(id);
            if(team !=null)
            {
                db.Teams.Remove(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the team with id " + id.ToString() + "!");
        }
        [NonAction]
        public List<CheckBoxViewModel> GetAllPlayers()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var player in db.Players.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = player.PlayerId,
                    Name = player.FirstName + ' ' + player.LastName,
                    Checked = false
                });
            }
            return checkboxList;
        }
    }
}