using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using BoxOfVegs.Web.ViewModels;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOfVegs.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        
        BOVContext context = new BOVContext();
       
        ServicesForCategories services = new ServicesForCategories();
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult Categories(string search, int? pageNO)
        {
            CategoryPagingModels model = new CategoryPagingModels();

            model.Searching = search;
            int pageSize = 5;
           
            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            var totalRec = services.GetCountOfCategories(search);
            model.category = services.GetCategoriesList(search, pageNO.Value);
            if (model.category != null)
            {
                model.Pager = new Pager(totalRec, pageNO, pageSize);

                return PartialView("Categories", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

       
        [HttpGet]
        public ActionResult AddCategory()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category, HttpPostedFileBase file)
        {
            string fileName = null;
            if (file != null)
            {
                fileName = System.IO.Path.GetFileName(file.FileName);

                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/category/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            if (ModelState.IsValid)
            {

                category.ImageUrl = fileName;
            services.AddCategory(category);
            return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpdateCategory(int categoryID)
        {
           
           var category= services.GetCategory(categoryID);
            category.ImageUrl = category.ImageUrl;
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdateCategory(Category category, HttpPostedFileBase file)
        {
            string fileName = null;
            if (file != null)
            {
                fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/category/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            //if(category.CategoryName==null)
            //{
            //    ModelState.AddModelError("", "Category name is required");

            //}
            if (ModelState.IsValid)
            {
                category.ImageUrl = file != null ? fileName : category.ImageUrl;


                services.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult DeleteCategory(int categoryID)
        {
            

            services.DeleteCategoryWithRange(categoryID);

            return RedirectToAction("Categories");
        }

    }
}