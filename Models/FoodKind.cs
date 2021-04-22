using System;
using System.Collections.Generic;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class FoodKind
    {
        public FoodKind()
        {
            Foods = new HashSet<Food>();
        }

        public int FoodKindId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
