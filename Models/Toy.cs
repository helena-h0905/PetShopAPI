using System;
using System.Collections.Generic;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class Toy
    {
        public int ToyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AnimalId { get; set; }
        public decimal? Toy { get; set; }

        public virtual Animal Animal { get; set; }
    }
}
