using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{

    public class City
    {
        public int CityID { get; set; }
        public virtual IList  <Mine> Mines { get; set; }
        public virtual IList  <Resource> Resources { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual IList<Building> Buildings { get; set; }
        public virtual IList <Troup> Troups { get; set; }
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

        public double GetProductionPerHour(int? level = null)
        {
            return (level ?? this.Level) * 13;
        }
        public DateTime UpgradeCompletesAt { get; set; }
        public bool IsUpgrading { get { return this.UpgradeCompletesAt > DateTime.Now; } }
        internal (int amount, ResourceType type) [] getUpgradeRequirements()
        {
            return new[]
            {
                (10* (this.Level+1), ResourceType.Clay),
                (10* (this.Level+1), ResourceType.Iron),
                (10* (this.Level+1), ResourceType.Wheat),
                (10* (this.Level+1), ResourceType.Wood),

            };
        }
    }

    public enum ResourceType
    {
        Wheat,
        Iron,
        Clay,
        Wood
    }

    public class Building
    {
        public int BuildingId { get; set; }
        public int Level { get; set; }
        public int? BuildingTypeId { get; set; }
        public virtual BuildingType BuildingType { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }

    public class BuildingType
    {
        public int BuildingTypeId { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Troup
    {
        public int TroupId { get; set; }
        public int TroupTypeId { get; set; }
        public virtual TroupType TroupType { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int TroupCount { get; set; }

    }

    public class TroupType
    {
        public int TroupTypeId { get; set; }
        [Required]
        [StringLength(15)]
        [MinLength(5)]
        [RegularExpression("[A-z]*")]
        public string Name { get; set; }
        [Range(0, 100)]
        public double Attack { get; set; }
        [Range(0, 100)]
        public double Defence { get; set; }
        [Range(0, 100)]
        public int CreationSpeed { get; set; }
    }
    public class CityFilterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? MinTroupCount { get; set; }
        public int? MaxTroupCount { get; set; }

        public List<City> Results { get; set; }
    }
}