using BoxOfVegs.Database;
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
    public class ProductController : Controller
    {
      
        ServicesForProducts services = new ServicesForProducts();
        // GET: Product
        public ActionResult Index()
        {
            if(Convert.ToInt32(Session["UserRoleID"]) == 1)
            { 
            return View();
            }
            return RedirectToAction("Login", "User");
        }


        public ActionResult Products(string search, int? pageNO)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {

                ProductPagingModels model = new ProductPagingModels
            {
                Searching = search
            };
            int pageSize = 5;

            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            var totalRec = services.GetCountOfProducts(search);
            model.Products = services.GetProductsList(search, pageNO.Value);
            if (model.Products != null)
            {
                model.Pager = new Pager(totalRec, pageNO, pageSize);

                return PartialView("Products", model);
            }
            else
            {
                return HttpNotFound();
            }
            }
            return RedirectToAction("Login", "User");

        }

        public List<SelectListItem> GetCategories()
        {
            ServicesForCategories categoryService = new ServicesForCategories();
            List<SelectListItem> list = new List<SelectListItem>();
            var categories = categoryService.AllCategories();
            foreach (var item in categories)
            {
                list.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                ViewBag.CategoryList = GetCategories();
            return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddProduct(Product product, HttpPostedFileBase file)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                if (ModelState.IsValid)
            {
                string fileName = null;
            if (file != null)
            {
                        string extension = System.IO.Path.GetExtension(file.FileName);
                        string newName = Guid.NewGuid().ToString();
                        fileName = newName + extension;

                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/product/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            
                product.ImageUrl = fileName;
            services.AddProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View();
            }
            return RedirectToAction("Login", "User");
        
        }

        public ActionResult EditProduct(int productID)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {

                ViewBag.CategoryList = GetCategories();
            var product = services.GetProduct(productID);
            product.ImageUrl = product.ImageUrl;
            return View(product);
            }
            return RedirectToAction("Login", "User");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditProduct(Product product, HttpPostedFileBase file)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                if (ModelState.IsValid)
                {
                string fileName = null;
                if (file != null)
                {
                        string extension = System.IO.Path.GetExtension(file.FileName);
                        string newName = Guid.NewGuid().ToString();
                        fileName = newName + extension;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/product/"), fileName);
                // file is uploaded
                file.SaveAs(path);
                        product.ImageUrl = fileName;
                }
                else
                {
                        var products = services.GetProduct(product.ProductID);
                        product.ImageUrl = products.ImageUrl;
                }
                
             
            services.UpdateProduct(product);
            return RedirectToAction("Index");
             }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View();
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult DeleteProduct(int productID)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                var product=services.GetProduct(productID);
                return View(product);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product product)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                services.RemoveProduct(product);
            return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public ActionResult ProductDetails(int productId)
        {
            BOVContext context = new BOVContext();
            HomeViewModels model = new HomeViewModels();

            model.Product = services.GetProduct(productId);
            model.Product.ImageUrl = model.Product.ImageUrl;
            model.Reviews = services.GetProductReview(productId);
            if (context.UserReviews.Any(u => u.ProductID == productId))
            {
                model.AverageRating = services.GetAverageRating(productId); ;
            }
            else
            {
                model.AverageRating = 0;
            }
            model.UserCount = services.GetCountUserReview(productId);

            if (model.Product == null)
            {
                return HttpNotFound();
            }
            else
            { 

            return View(model);
            }
        }
        [HttpPost]
        public ActionResult AddReview(HomeViewModels model,float rating)
        {
            UserReview review = new UserReview();

            if (Session["UserRoleID"] != null)
            {
                
                    review.UserID = Convert.ToInt32(Session["UserID"]);
                    review.ProductID = model.Product.ProductID;
                    review.PostDate = DateTime.Now;
                    review.Review = model.Review;
                    review.Rating = rating;
                    services.AddReview(review);
               
                return RedirectToAction("ProductDetails", "Product",new { productId = model.Product.ProductID });
            }
            return RedirectToAction("Login", "User");

        }
    }
}