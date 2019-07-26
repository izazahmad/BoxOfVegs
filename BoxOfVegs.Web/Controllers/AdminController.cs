using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using BoxOfVegs.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOfVegs.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public RepositoryWork repoWork = new RepositoryWork();
        public ActionResult Index()
        {
            return View("Dashboard");
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

        public ActionResult Categories()
        {
            List<Category> allcategories = repoWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable().ToList();
            return View(allcategories);
        }

        //public ActionResult AddCategory(Category category)
        //{
        //    repoWork.GetRepositoryInstance<Category>().AddRecord(category);
        //    return RedirectToAction("Categories");
        //}
        //public ActionResult UpdateCategory(int categoryID)
        //{
        //    Category category;

        //    category = JsonConvert.DeserializeObject<Category>(JsonConvert.SerializeObject(repoWork.GetRepositoryInstance<Category>().GetFirstorDefault(categoryID)));

        //    return View("UpdateCategory", category);
        //}
        [HttpGet]
        public ActionResult AddCategory()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            repoWork.GetRepositoryInstance<Category>().AddRecord(category);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public ActionResult UpdateCategory(int categoryID)
        {
            var category = repoWork.GetRepositoryInstance<Category>().GetRecord(categoryID);
            return View(category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {

            repoWork.GetRepositoryInstance<Category>().Update(category);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public ActionResult DeleteCategory(int categoryID)
        {
            var category = repoWork.GetRepositoryInstance<Category>().GetRecord(categoryID);
            return View(category);
        }
        [HttpPost]
        public ActionResult DeleteCategory(Category category)
        {
            
            repoWork.GetRepositoryInstance<Category>().Remove(category);
            return RedirectToAction("Categories");
        }
        
    }
}