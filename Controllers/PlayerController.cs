using lab3_miercuri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_ShirtStore.Models;

namespace T_ShirtStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlayerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Player
        [HttpGet]
        public ActionResult Index()
        {
            List<Player> players = db.Players.ToList();
            ViewBag.Players = players;
            return View();
        }
        public ActionResult Details(int? id)
        {
            if(id.HasValue)
            {
                Player player = db.Players.Find(id);
                if(player != null)
                {
                    return View(player);
                }
                return HttpNotFound("Couldn't find the player with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing player id parameter!");
        }
        [HttpGet]
        public ActionResult New()
        {
            Player player = new Player();
            player.TeamsList = GetAllTeams();
            return View(player);
        }
        [HttpPost]
        public ActionResult New(Player playerRequest)
        {
            if (playerRequest.TeamsList != null)
            {
                var selectedTeams = playerRequest.TeamsList.Where(b => b.Checked).ToList();
                try
                {
                    if (ModelState.IsValid)
                    {
                        playerRequest.Teams = new List<Team>();
                        for (int i = 0; i < selectedTeams.Count(); i++)
                        {
                            
                            Team team = db.Teams.Find(selectedTeams[i].Id);
                            playerRequest.Teams.Add(team);
                        }
                        db.Players.Add(playerRequest);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(playerRequest);
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                    return View(playerRequest);
                }
            }
            else
            {
                db.Players.Add(playerRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playerRequest);

        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Player player = db.Players.Find(id);
                player.TeamsList = GetAllTeams();
                foreach(Team checkedTeam in player.Teams)
                {
                    player.TeamsList.FirstOrDefault(g => g.Id == checkedTeam.TeamId).Checked = true;

                }
                if(player == null)
                {
                    return HttpNotFound("Coludn't find the player with id " + id.ToString() + "!");
                }
                return View(player);
            }
            return HttpNotFound("Missing player id parameter");
        }
        [HttpPost]
        public ActionResult Edit(int id,Player playerRequest)
        {
            Player player = db.Players.SingleOrDefault(b => b.PlayerId.Equals(id));
            var selectedTeams = playerRequest.TeamsList.Where(b => b.Checked).ToList();
            try
            {
                if(ModelState.IsValid)
                {
                    if(TryUpdateModel(player))
                    {
                        player.FirstName = playerRequest.FirstName;
                        player.LastName = playerRequest.LastName;
                        player.Teams.Clear();
                        player.Teams = new List<Team>();
                        for(int i=0;i< selectedTeams.Count();i++)
                        {
                            Team team = db.Teams.Find(selectedTeams[i].Id);
                            player.Teams.Add(team);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(playerRequest);
            }
            catch(Exception)
            {
                return View(playerRequest);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Player player = db.Players.Find(id);
            if (player != null)
            {
                db.Players.Remove(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the player with id " + id.ToString() + "!");
        }
        [NonAction]
        public List<CheckBoxViewModel> GetAllTeams()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach(var team in db.Teams.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = team.TeamId,
                    Name = team.TeamName,
                    Checked = false
                });
            }
            return checkboxList;
        }
    }
}