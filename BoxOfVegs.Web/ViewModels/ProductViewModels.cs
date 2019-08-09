using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
   
    
    public class ProductPagingModels
    {
        public List<Product> Products { get; set; }
        public string Searching { get; set; }
        public Pager Pager { get; set; }
    }
}