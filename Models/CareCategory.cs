using System;
using System.Collections.Generic;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class CareCategory
    {
        public CareCategory()
        {
            CareSupplies = new HashSet<CareSupply>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CareSupply> CareSupplies { get; set; }
    }
}
