using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using BoxOfVegs.Web.ViewModels;
using Rotativa.MVC;
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
        BOVContext context = new BOVContext();

        public ActionResult Index(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy)
        {
           

            ShopProductViewModel model = new ShopProductViewModel
            {
                ShopSearching = search,
                ShopCategories = categoryService.AllCategories(),
                MaximumPrice = productService.GetMaxPrice()
            };

            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            model.ShopProducts = productService.ShopProducts(search, minPrice, maxPrice, categoryID, sortBy);


            return View(model);
        }
        public ActionResult AddInCart(int productId, int qty)
        {
            List<CartViewModel> list = new List<CartViewModel>();
            var product = productService.GetProduct(productId);
            CartViewModel crt = new CartViewModel
            {
                ProductID = product.ProductID,
                Price = product.Price,
                Quanity = qty,
                TotalQuantity=product.Quantity,
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
                List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
                int change = 0;
                foreach (var item in newlist)
                {
                    if (item.ProductID == crt.ProductID)
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
            
            return RedirectToAction("Cart");
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
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
            if (Session["UserID"] != null)
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
            else
            {
                return RedirectToAction("Login", "User");
            }
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
        public ActionResult Checkout( FormCollection formData)
        {
            
                if (Session["UserID"] != null)
                {
                  if(Session["cart"] != null)
                  {
                   
                       List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
                    int userID = Convert.ToInt32(Session["UserID"].ToString());

                    Invoice invoice = new Invoice
                    {
                        
                        UserID = userID,
                        TotalAmount = (decimal)Session["total"],
                        InvoiceDate = DateTime.Now,
                        Address = Convert.ToString(formData["street"]),
                        City = Convert.ToString(formData["city"]),
                        PostCode = Convert.ToString(formData["postCode"]),
                        PhoneNumber = Convert.ToString(formData["phone"])
                    };
                    shopService.AddInvoice(invoice);
                    foreach (var item in newlist)
                    {
                        Order orders = new Order
                        {
                            ProductID = item.ProductID,
                            InvoiceID = invoice.InvoiceID,
                            ProductName = item.ProductName,
                            Quantity = item.Quanity,
                            Date = DateTime.Now,
                            UnitPrice = item.Price,
                            Subtotal = item.Subtotal
                        };
                        int newQuantity = (item.TotalQuantity - item.Quanity);
                         shopService.AddOrder(orders);
                        productService.UpdateQuantity(item.ProductID,newQuantity);
                    }
                    Session.Remove("total");
                    Session.Remove("cart");
                    return RedirectToAction("Invoice", new { userId = userID });
                   
                }
                else
                {
                    return RedirectToAction("Cart");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        public ActionResult UpdateCart(FormCollection formData)
        {
            string[] quantities = formData.GetValues("qty");
            List<CartViewModel> newlist = (List<CartViewModel>)Session["cart"];
            decimal x = 0;
            for (int i = 0; i < newlist.Count; i++)
            {
                newlist[i].Quanity = Convert.ToInt32(quantities[i]);
                newlist[i].Subtotal = newlist[i].Price * newlist[i].Quanity;
                Session["cart"] = newlist;
                x += newlist[i].Subtotal;

            }



            Session["total"] = x;
            return View("Cart");
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult CreateInvoice(int userId)
        {
            
            var invoiceDetails = shopService.GetInvoiceDetail(userId);
            var userDetails = shopService.GetUserDetails(userId);
            int invoiceId = invoiceDetails.InvoiceID;
            InvoiceDetailViewModel model = new InvoiceDetailViewModel
            {
                Orders = shopService.GetOrders(invoiceId),
                UserID = userId,
                InvoiceDate = invoiceDetails.InvoiceDate,
                InvoiceID = invoiceId,
                PhoneNumber = invoiceDetails.PhoneNumber,
                PostCode = invoiceDetails.PostCode,
                TotalAmount = invoiceDetails.TotalAmount,
                Address = invoiceDetails.Address,
                City = invoiceDetails.City,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName
            };

            return PartialView("CreateInvoice", model);
        }
        public ActionResult PrintInvoice(int userid)
        {
            return new ActionAsPdf("CreateInvoice", new { userid });

        }
        public ActionResult YourOrder()
        {
            if (Session["UserID"] != null)
            {
                int userId = Convert.ToInt32(Session["UserID"].ToString());
                var orderRecords = context.Orders.Join(context.Invoices, o => o.InvoiceID, i => i.InvoiceID, (o, i) => new { o, i })
                                                .Where(u => u.i.UserID == userId).OrderByDescending(u=>u.i.InvoiceDate)
                                                .Select(u => new OrdersVeiwModel
                                                {
                                                    OrderID= u.o.OrderID,
                                                    ProductID= u.o.ProductID,
                                                    ProductName= u.o.ProductName,
                                                    Quantity=u.o.Quantity,
                                                    Product= u.o.Product,
                                                    Subtotal=u.o.Subtotal,
                                                    UnitPrice= u.o.UnitPrice,
                                                    Invoice=u.o.Invoice,
                                                    InvoiceDate=u.i.InvoiceDate,
                                                    UserID= u.i.UserID,
                                                    User=u.i.User,
                                                    PostCode= u.i.PostCode,
                                                    Address=u.i.Address,
                                                    City=u.i.City
                                                });
                       return View(orderRecords.ToList());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}