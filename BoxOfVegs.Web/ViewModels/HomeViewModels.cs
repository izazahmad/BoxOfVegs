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
        //public List<Category> FeaturedCategories { get; set; }
        public List<Product> ProductByCategoryName { get; set; }
        public List<UserReview> Reviews { get; set; }
        //public string Searching { get; set; }
        //public Pager Pager { get; set; }
        public Product Product { get; set; }
        //public int UserReviewID { get; set; }
        //public int ProductID { get; set; }
        //public int UserID { get; set; }
        //public string UserName { get; set; }

        public string Review { get; set; }
        public float? AverageRating { get; set; }
        public int? UserCount { get; set; }
        //public DateTime PostDate { get; set; }
    }
    
}