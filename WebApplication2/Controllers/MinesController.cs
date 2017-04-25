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
    }
}