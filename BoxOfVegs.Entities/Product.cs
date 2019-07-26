﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string ProductName { get; set; }

        [Range(1,100000)]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }

        public string ImageUrl{ get; set; }

        public bool isFeatured { get; set; }

        [Range(1,500)]
        public int Quantity { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}
