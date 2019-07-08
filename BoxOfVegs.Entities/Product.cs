using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class Product : BaseEnitity
    {
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int ProductImage{ get; set; }
    }
}
