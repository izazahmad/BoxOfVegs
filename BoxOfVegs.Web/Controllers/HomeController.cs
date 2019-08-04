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

        public RepositoryWork repoWork = new RepositoryWork();
        BOVContext context = new BOVContext();
        CategoriesService categoryService = new CategoriesService();
        public ActionResult Index(string search, int? page)
        {
            
            HomeViewModels model = new HomeViewModels();
            return View(model.CreateModel(search, 4, page));
        }


        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = context.Products.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = context.Products.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            return Redirect("Index");
        }

    }
}