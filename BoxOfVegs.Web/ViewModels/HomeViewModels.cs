using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
    public class HomeViewModels
    {
        

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> ProductByCategoryName { get; set; }
        public string Searching { get; set; }
        public Pager Pager { get; set; }
        public Product Product { get; set; }
    }
    
}