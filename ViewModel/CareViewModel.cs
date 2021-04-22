using System;

namespace PetShopAPI.ViewModel
{
    public class CareViewModel
    {
        public int CareId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AnimalId { get; set; }
        public string AnimalName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? Price { get; set; }
    }
}