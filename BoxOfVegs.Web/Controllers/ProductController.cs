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
       public RepositoryWork repoWork = new RepositoryWork();
        ProductService productsService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View("Products");
        }


        public ActionResult Products()
        {
            List<Product> allProducts = repoWork.GetRepositoryInstance<Product>().GetAllRecordsIQueryable().ToList();
            return View(allProducts);
            
        }

        public List<SelectListItem> GetCategories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var categories = repoWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach (var item in categories)
            {
                list.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.CategoryList = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase file)
        {
            string fileName = null;
            if (file != null)
            {
                 fileName = System.IO.Path.GetFileName(file.FileName);
                
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/product/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }

            product.ImageUrl = fileName;
            //product.CreatedDate = DateTime.Now;
            repoWork.GetRepositoryInstance<Product>().AddRecord(product);
                return RedirectToAction("Products");
            
        }

        public ActionResult EditProduct(int productID)
        {
            
            ViewBag.CategoryList = GetCategories();
            var product = repoWork.GetRepositoryInstance<Product>().GetRecord(productID);
            product.ImageUrl = product.ImageUrl;
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase file)
        {
            string fileName = null;
            if (file != null)
            {
                fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/product/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            product.ImageUrl = file != null ? fileName : product.ImageUrl;
            //tbl.ModifiedDate = DateTime.Now;
            repoWork.GetRepositoryInstance<Product>().Update(product);
            return RedirectToAction("Products");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int productID)
        {
            var product = repoWork.GetRepositoryInstance<Product>().GetRecord(productID);
            return View(product);
        }
        [HttpPost]
        public ActionResult DeleteProduct(Product product)
        {

            repoWork.GetRepositoryInstance<Product>().Remove(product);
            return RedirectToAction("Products");
        }












        // GET: Product
        public ActionResult ProductTable(string search)
        {

            var products = productsService.GetProducts();
            if(!string.IsNullOrEmpty(search))
            {
                products=products.Where(p =>  p.ProductName.ToLower().Contains(search.ToLower()) && p.ProductName != null).ToList();
            }
            return PartialView(products);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            CategoriesService categoryService = new CategoriesService();
            var categories = categoryService.GetCategories();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModels model)
        {

            CategoriesService categoryService = new CategoriesService();
            var newProduct = new Product();
            newProduct.ProductName = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = categoryService.GetCategory(model.CategoryID);
            productsService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = productsService.GetProduct(Id);
            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            productsService.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }

    }
}