using BoxOfVegs.Services;
using BoxOfVegs.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOfVegs.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
       ServicesForCategories categoryService = new ServicesForCategories();
        ServicesForProducts productService = new ServicesForProducts();
        public ActionResult Index(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int? pageNO)
        {
            int pageSize = 12;
            // var pageSize = ConfigurationsService.Instance.ShopPageSize();

            ShopProductViewModel model = new ShopProductViewModel();

            model.ShopSearching = search;
            model.ShopCategories = categoryService.AllCategories();
            model.MaximumPrice = productService.GetMaxPrice();

            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = productService.ShopProductsCount(search, minPrice, maxPrice, categoryID, sortBy);
            model.ShopProducts = productService.ShopProducts(search, minPrice, maxPrice, categoryID, sortBy, pageNO.Value, pageSize);

            model.ShopPager = new Pager(totalCount, pageNO, pageSize);

            return View(model);
        }
        public ActionResult ShopProducts()
        {
            return View();
        }
        public ActionResult AddInCart(int productId, int qty)
        {
            //int qty = 2;
            List<CartViewModel> li = new List<CartViewModel>();
            var p = productService.GetProduct(productId);
            CartViewModel c = new CartViewModel();
            c.productid = p.ProductID;
            c.price = (float)p.Price;
            c.qty = /*Convert.ToInt32(qty)*/qty;
            c.bill = c.price * c.qty;
            c.productname = p.ProductName;
            if (Session["cart"] == null)
            {
                li.Add(c);
                Session["cart"] = li;

            }
            else
            {
                List<CartViewModel> li2 = (List<CartViewModel>)Session["cart"]; ;
                int change = 0;
                foreach (var item in li2)
                {
                    if (item.productid==c.productid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        change = 1;

                    }
                }
                if (change==0)
                {
                    li2.Add(c);
                }
                Session["cart"] = li2;
            }

            
            //List<Item> cart = new List<Item>();
            //var product = productService.GetProduct(productId);
            //if (Session["cart"] == null)
            //{

            //    cart.Add(new Item()
            //    {
            //        Product = product,
            //        Quantity = 1
            //    });
            //    Session["cart"] = cart;
            //}
            //else
            //{
            //    List<Item> cart2 = (List<Item>)Session["cart"];
            //    var product2 = productService.GetProduct(productId);

            //    foreach (var item in cart2)
            //    {
            //        if (item.Product.ProductID==productId ) 
            //        { 
            //           int lastQty = item.Quantity;
            //           cart2.Remove(item);
            //           cart2.Add(new Item()
            //           {
            //               Product = product,
            //               Quantity = lastQty+1
            //           });
            //            break;
            //        }
            //        else
            //        {
            //            cart2.Add(new Item()
            //            {
            //                Product = product,
            //                Quantity = 1
            //            });
            //            break;
            //        }

            //    }

            //    Session["cart"] = cart2;
            //}
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            if (Session["cart"] != null)
            {
                float x = 0;
                List<CartViewModel> li2 = (List<CartViewModel>)Session["cart"];
                foreach (var item in li2)
                {
                    x += item.bill;

                }

                Session["total"] = x;
            }

            return View();
        }
        public ActionResult DeleteFromCart(int productId)
        {
            List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.productid == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}