using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CitiesController : Controller
    {
        ApplicationDbContext ob = new ApplicationDbContext();

        // GET: Cities
        public ActionResult Index(CityFilterViewModel cityfilter)
        {
            IQueryable<City> query = ob.Cities;
            if(cityfilter.Name!=null)
            {
                query = query.Where(u => u.ApplicationUser.UserName.Contains(cityfilter.Name));
            }
            if(cityfilter.Email!=null)
            {
                query = query.Where(u => u.ApplicationUser.Email.Contains(cityfilter.Email));
            }
            if(cityfilter.MaxTroupCount!=null)
            {
                query = query.Where(c => c.Troups.Sum(t=> t.TroupCount)>0);
            }

            var orase2 = query.ToList();
            cityfilter.Results = orase2;
            /*var orase = ob.Cities.ToList();
            cityfilter.Results = orase;*/
            return View(cityfilter);
            
        }
    }
}