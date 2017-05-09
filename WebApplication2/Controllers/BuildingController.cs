using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BuildingController : Controller
    {
        ApplicationDbContext ob = new ApplicationDbContext();

        // GET: Building
        public ActionResult Index()
        {
            var userID = this.User.Identity.GetUserId();
            var user = ob.Users.Find(userID);
            var city = user.Cities.First();
            return View(city);
            //return View();
        }
        public ActionResult Build(int buildingID)
        {
            return View(new BuildViewModel
            {
                BuildingID = buildingID,
                BuildingTypes = this.ob
                    .BuildingTypes
                    .Select(b => new SelectListItem
                    {
                        Value = b.BuildingTypeId.ToString(),
                        Text = b.Name
                    })
            });
        }
    }

    public class BuildViewModel
    {
        public int BuildingID { get; set; }
        public IEnumerable <SelectListItem> BuildingTypes { get; set; }
        public int? SelectedBuildingType { get; set; }
    }
}