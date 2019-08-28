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

        //public RepositoryWork repoWork = new RepositoryWork();
        //BOVContext context = new BOVContext();
        //CategoriesService categoryService = new CategoriesService();
        ServicesForProducts services = new ServicesForProducts();
        ServicesForCategories categoryServices = new ServicesForCategories();
        public ActionResult Index()
        {
            
            HomeViewModels model = new HomeViewModels();
            model.Products = services.GetNewAddedProducts(4);
            model.Categories = categoryServices.AllCategories();
            //model.FeaturedCategories = categoryServices.GetFeaturedCategory();
            model.ProductByCategoryName = services.GetProductByCatName("Box");
            return View(model);
            //return View(model.CreateModel(search, 4, pageNO));

            //HomeViewModels model = new HomeViewModels();

            // model.Searching = search;
            //    int pageSize = 5;

            //    pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            //    var totalRec = services.GetCountOfProducts(search);
            //    model.Products = services.GetProductsList(search, pageNO.Value);
            //    if (model.Products != null)
            //    {
            //        model.Pager = new Pager(totalRec, pageNO, pageSize);

            //        return PartialView("Products", model);
            //    }
            //    else
            //    {
            //        return HttpNotFound();
            //    }

            //}
        }


        //public ActionResult AddInCart(int productId)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<Item> cart = new List<Item>();
        //        var product = context.Products.Find(productId);
        //        cart.Add(new Item()
        //        {
        //            Product = product,
        //            Quantity = 1
        //        });
        //        Session["cart"] = cart;
        //    }
        //    else
        //    {
        //        List<Item> cart = (List<Item>)Session["cart"];
        //        var product = context.Products.Find(productId);
        //        cart.Add(new Item()
        //        {
        //            Product = product,
        //            Quantity = 1
        //        });
        //        Session["cart"] = cart;
        //    }
        //    return Redirect("Index");
        //}

    }
}