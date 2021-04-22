using System;
using System.Collections.Generic;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class Food
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AnimalId { get; set; }
        public int? NumberOfPackagesInStorage { get; set; }
        public decimal? PackageWeightInKg { get; set; }
        public DateTime? EarliestExpirationDate { get; set; }
        public int? FoodKindId { get; set; }
        public decimal? Price { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual FoodKind FoodKind { get; set; }
    }
}
