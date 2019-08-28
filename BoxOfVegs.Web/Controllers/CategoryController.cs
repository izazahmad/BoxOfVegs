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
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }

       

        public ActionResult Categories(string search, int? pageNO)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
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
            return RedirectToAction("Login", "User");
        }

       
        [HttpGet]
        public ActionResult AddCategory()
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {

                return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category, HttpPostedFileBase file)
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
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/category/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            

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
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public ActionResult UpdateCategory(int categoryID)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {

                var category= services.GetCategory(categoryID);
            category.ImageUrl = category.ImageUrl;
            return View(category);
            }
            return RedirectToAction("Login", "User");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdateCategory(Category category, HttpPostedFileBase file)
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
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/category/"), fileName);
                // file is uploaded
                file.SaveAs(path);
            }
            //if(category.CategoryName==null)
            //{
            //    ModelState.AddModelError("", "Category name is required");

            //}
            
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
        public ActionResult DeleteCategory(int categoryID)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                var category = services.GetCategory(categoryID);

                return View(category);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult DeleteCategory(Category category)
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {

                services.DeleteCategoryWithRange(category);

            return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "User");
        }

    }
}