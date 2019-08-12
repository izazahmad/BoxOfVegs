using BoxOfVegs.Entities;
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
        ServicesForShop shopService = new ServicesForShop();
        public ActionResult Index(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int? pageNO)
        {
            int pageSize = 12;
            // var pageSize = ConfigurationsService.Instance.ShopPageSize();

            ShopProductViewModel model = new ShopProductViewModel
            {
                ShopSearching = search,
                ShopCategories = categoryService.AllCategories(),
                MaximumPrice = productService.GetMaxPrice()
            };

            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = productService.ShopProductsCount(search, minPrice, maxPrice, categoryID, sortBy);
            model.ShopProducts = productService.ShopProducts(search, minPrice, maxPrice, categoryID, sortBy, pageNO.Value, pageSize);

            model.ShopPager = new Pager(totalCount, pageNO, pageSize);

            return View(model);
        }
        
        public ActionResult AddInCart(int productId, int qty)
        {
            //int qty = 2;
            List<CartViewModel> list = new List<CartViewModel>();
            var product = productService.GetProduct(productId);
            CartViewModel crt = new CartViewModel
            {
                ProductID = product.ProductID,
                Price = product.Price,
                Quanity = qty,
                ProductURL=product.ImageUrl
            };
            crt.Subtotal = crt.Price * crt.Quanity;
            crt.ProductName = product.ProductName;
            if (Session["cart"] == null)
            {
                list.Add(crt);
                Session["cart"] = list;

            }
            else
            {
                List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"]; ;
                int change = 0;
                foreach (var item in newlist)
                {
                    if (item.ProductID== crt.ProductID)
                    {
                        item.Quanity += crt.Quanity;
                        item.Subtotal += crt.Subtotal;
                        change = 1;

                    }
                }
                if (change==0)
                {
                    newlist.Add(crt);
                }
                Session["cart"] = newlist;
            }

            if (Session["cart"] != null)
            {
                Decimal x = 0;
                List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
                foreach (var item in newlist)
                {
                    x += item.Subtotal;

                }

                Session["total"] = x;
            }
            
            return RedirectToAction("Checkout");
        }
       
        public ActionResult DeleteFromCart(int productId)
        {
            List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.ProductID == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            if (Session["cart"] != null)
            {
                Decimal x = 0;
                List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
                foreach (var item in newlist)
                {
                    x += item.Subtotal;

                }

                Session["total"] = x;
            }
            return RedirectToAction("Checkout");
        }
        public ActionResult Checkout()
        {
            if (Session["cart"] != null)
            {
                Decimal x = 0;
                List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
                foreach (var item in newlist)
                {
                    x += item.Subtotal;

                }

                Session["total"] = x;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            if (Session["UserID"] != null)
            {
                if(Session["cart"] != null)
                { 
                List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
                foreach (var item in newlist)
                {
                    Order orders = new Order();
                    orders.UserID = Convert.ToInt32(Session["UserID"].ToString());
                    orders.ProductID = item.ProductID;
                    orders.Quantity = item.Quanity;
                    orders.Date = DateTime.Now;
                    orders.UnitPrice = item.Price;
                    orders.Subtotal = item.Subtotal;
                    shopService.AddOrder(orders);
                }
                Session.Remove("total");
                Session.Remove("cart");
                return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}