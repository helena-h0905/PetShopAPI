using System;

namespace PetShopAPI.ViewModel
{
    public class FoodViewModel
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AnimalId { get; set; }
        public string AnimalName { get; set; }
        public int? NumberOfPackagesInStorage { get; set; }
        public decimal? PackageWeightInKg { get; set; }
        public DateTime? EarliestExpirationDate { get; set; }
        public int? FoodKindId { get; set; }
        public string FoodKindName { get; set; }
        public decimal? Price { get; set; }
    }
}