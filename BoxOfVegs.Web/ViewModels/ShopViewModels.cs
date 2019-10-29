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

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductURL { get; set; }
        public int TotalQuantity { get; set; }
        public Decimal Price { get; set; }
        public int Quanity { get; set; }
        public Decimal Subtotal { get; set; }
        
    }
    public class OrdersVeiwModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal UnitPrice { get; set; }
        public Invoice Invoice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

    }

}