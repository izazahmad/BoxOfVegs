using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using BoxOfVegs.Web.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOfVegs.Web.Controllers
{
    public class HomeController : Controller
    {
        //create object instance of the ServicesForProducts service class for products
        ServicesForProducts services = new ServicesForProducts();
        //create object instance of the ServicesForCategories Service class for categories
        ServicesForCategories categoryServices = new ServicesForCategories();
        public ActionResult Index()
        {
            //object instance for the HomeViewModels class
            HomeViewModels model = new HomeViewModels
            {
                //call service class and get 4 top most new added product from the database and store in list<product>
                Products = services.GetNewAddedProducts(4),
                //call service class to get all categories from the database and store in list<Category>
                Categories = categoryServices.AllCategories(),
                //call service class to get product which is belong to box category from the database
                ProductByCategoryName = services.GetProductByCatName("Box")
            };
            // return to index view page with all the data which is in model object
            return View(model);        
        }       
    }
}