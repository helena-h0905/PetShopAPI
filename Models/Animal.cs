using System;
using System.Collections.Generic;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CareSupply> CareSupplies { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<Toy> Toys { get; set; }
    }
}
