using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
   public class AddProductViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        [MinLength(3), MaxLength(50)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Price is required for the product")]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category is required")]

        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }

        public string ImageUrl { get; set; }

        [Range(1, 500)]
        public int Quantity { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
    
    public class ProductPagingModels
    {
        public List<Product> Products { get; set; }
        public string Searching { get; set; }
        public Pager Pager { get; set; }
    }
}