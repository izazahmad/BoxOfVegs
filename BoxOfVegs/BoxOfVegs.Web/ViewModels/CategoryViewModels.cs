using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{

    public class CategoryPagingModels
    {
        
        public List<Category> category { get; set; }
        
        public string Searching { get; set; }
        public Pager Pager { get; set; }
        
    }
    public class CategoryViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        
    }
   
}