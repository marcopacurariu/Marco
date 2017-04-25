using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class City
    {
        public int CityID { get; set; }
        public virtual IList  <Mine> Mines { get; set; }
        public virtual IList  <Resource> Resources { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserID { get; set; }
    }

    public class Resource
    {
        public int ResourceID { get; set; }
        public virtual City City { get; set; }
        public int CityID { get; set; }
        public DateTime LastUpdate { get; set; }
        public ResourceType Type { get; set; }

        public double Value { get; set; }
    }

    public class Mine
    {
        public int MineID { get; set; }
        public int CityID { get; set; }
        public virtual City City { get; set; }

        public int Level { get; set; }

        public ResourceType Type { get; set; }
    }

    public enum ResourceType
    {
        Wheat,
        Iron,
        Clay,
        Wood
    }
}