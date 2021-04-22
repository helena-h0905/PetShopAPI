using System;
using System.Collections.Generic;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class CareSupply
    {
        public int CareSupplyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AnimalId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual CareCategory Category { get; set; }
    }
}
