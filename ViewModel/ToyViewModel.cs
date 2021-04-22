using System;

namespace PetShopAPI.ViewModel
{
    public class ToyViewModel
    {
        public int ToyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AnimalId { get; set; }
        public string AnimalName { get; set; }
    }
}