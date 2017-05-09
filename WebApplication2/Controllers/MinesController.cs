using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class MinesController : Controller
    {
        // GET: Mines
        ApplicationDbContext ob = new ApplicationDbContext();
        public ActionResult Index()
        {
            
            var userID = this.User.Identity.GetUserId();
            var user = ob.Users.Find(userID);
            var city = user.Cities.First();
            this.UpdteResources(city);
            return View(city);


        }
        private void UpdteResources(City city)
        {
            var start = DateTime.Now;
            foreach (var res in city.Resources)
            {
                foreach (var mine in city.Mines)
                {
                    if (mine.Type == res.Type)
                    {
                        res.Value += mine.GetProductionPerHour() * (start - res.LastUpdate).TotalHours;
                    }
                }
                res.LastUpdate = start;
            }
            ob.SaveChanges();

        }

        public ActionResult Details(int mineID)
        {
            var userID = this.User.Identity.GetUserId();
            var user = ob.Users.Find(userID);
            var city = user.Cities.First();
            var mine = ob.Mines.Find(mineID);
            return View (mine);
        }
        [HttpPost]
        public ActionResult Upgrade (int mineID, bool fastUpgrade)
        {
            var mine = ob.Mines.Find(mineID);
            var city = mine.City;
            var needed = mine.getUpgradeRequirements();
            var have = city.Resources;

            if(fastUpgrade)
            {
                needed = needed.Select(n => (amount: n.amount * 10, type: n.type)).ToArray();
            }

            var r = needed
                .Join(have, n => n.type, h => h.Type, (n, h) => (needed: n, have: h));

            if (!r.All(_ => _.needed.amount > _.have.Value))
            {
                return View(new MessageViewModel
                {
                    Message = $"You do not have enough resources"
                });

            }

            foreach (var item in r)
            {
                item.have.Value -= item.needed.amount;
            }
            var amounts = needed.Select(n => n.amount);
            mine.Level++;
            mine.UpgradeCompletesAt = DateTime.Now.AddHours(0.5 * mine.Level);

            this.ob.SaveChanges();
            return View(new MessageViewModel
            {
                Message = $"Mine id = {mineID}{fastUpgrade}"
            });
        }
    }
}