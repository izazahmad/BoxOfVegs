using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
    public class ShopProductViewModel
    {
        public int MaximumPrice { get; set; }
        public List<Category> ShopCategories { get; set; }
        public List<Product> ShopProducts { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }

        public Pager ShopPager { get; set; }
        public string ShopSearching { get; set; }
    }
    public class CartViewModel
    {
        public int productid { get; set; }
        public string productname { get; set; }

        public float price { get; set; }
        public int qty { get; set; }
        public float bill { get; set; }
    }
       
}