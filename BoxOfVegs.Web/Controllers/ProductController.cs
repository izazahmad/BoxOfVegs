﻿using BoxOfVegs.Database;
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
      // public RepositoryWork repoWork = new RepositoryWork();
       // ProductService services = new ProductService();
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
            //var categories = repoWork.GetRepositoryInstance<Category>().GetAllRecords();
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
                        //fileName = System.IO.Path.GetFileName(file.FileName);
                        fileName = newName + extension;

                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/product/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            
                product.ImageUrl = fileName;
            //product.CreatedDate = DateTime.Now;
            //repoWork.GetRepositoryInstance<Product>().AddRecord(product);
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
           // var product = repoWork.GetRepositoryInstance<Product>().GetRecord(productID);
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
                        //fileName = System.IO.Path.GetFileName(file.FileName);
                        fileName = newName + extension;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/product/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            
                product.ImageUrl = file != null ? fileName : product.ImageUrl;
            //tbl.ModifiedDate = DateTime.Now;
           // repoWork.GetRepositoryInstance<Product>().Update(product);
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
                //repoWork.GetRepositoryInstance<Product>().Remove(product);
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
                //repoWork.GetRepositoryInstance<Product>().Remove(product);
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
            //float? average= services.GetAverageRating(productId);
            if (context.UserReviews.Any(u => u.ProductID == productId))
            {
                model.AverageRating = services.GetAverageRating(productId); ;
            }
            else
            {
                model.AverageRating = 0;
            }
            //model.AverageRating = services.GetAverageRating(productId);
            model.UserCount = services.GetCountUserReview(productId);
            //model.UserName = from u in context.Users where u.UserID == model.Reviews.UserID select u.UserName;

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
        //[ValidateAntiForgeryToken]

        public ActionResult AddReview(HomeViewModels model,float rating)
        {
            UserReview review = new UserReview();

            if (Session["UserRoleID"] != null)
            {
                //if (ModelState.IsValid)
                //{
                    review.UserID = Convert.ToInt32(Session["UserID"]);
                    review.ProductID = model.Product.ProductID;
                    review.PostDate = DateTime.Now;
                    review.Review = model.Review;
                    review.Rating = rating;
                    services.AddReview(review);
                //}
                //else
                //{
                //    ModelState.AddModelError("", "");
                //}
                return RedirectToAction("ProductDetails", "Product",new { productId = model.Product.ProductID });
            }
            return RedirectToAction("Login", "User");

        }
    }
}