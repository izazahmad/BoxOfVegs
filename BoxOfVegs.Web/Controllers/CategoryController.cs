using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOfVegs.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService categoryService = new CategoriesService();
        //GET: Category
        public RepositoryWork _unitOfWork = new RepositoryWork();

       [HttpGet]
        public ActionResult Index()
        {
            //var categories = categoryService.GetCategories();
            //return View(categories);

            List<Category> allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable().ToList();
            return View(allcategories);

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            
            categoryService.SaveCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var category = categoryService.GetCategory(Id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {

            categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var category = categoryService.GetCategory(Id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            category = categoryService.GetCategory(category.CategoryID);
            categoryService.DeleteCategory(category.CategoryID);
            return RedirectToAction("Index");
        }
    }
}