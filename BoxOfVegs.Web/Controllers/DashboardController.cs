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
    public class DashboardController : Controller
    {
        // GET: Admin
        public RepositoryWork repoWork = new RepositoryWork();
        public ActionResult Index()
        {
            return View();
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
        
        public ActionResult Categories(string search, int? pageNO)
        {
            CategoryPagingModels model = new CategoryPagingModels();

             model.Searching = search;
            int pageSize = 5;
            //BOVContext context = new BOVContext();

            ////SqlParameter[] param = new SqlParameter[]{
            ////  new SqlParameter("@search",search??(object)DBNull.Value)
            ////};
            ////model.ListOfCategory = repoWork.GetRepositoryInstance<Category>().GetResultSqlprocedure("searchCategory @search", param).ToPagedList(page ?? 1, 4);
            //IPagedList<Category> data = context.Database.SqlQuery<Category>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, 5);

            //return View(data);
            //if (!string.IsNullOrEmpty(search))
            //{
            // model.ListOfCategory = repoWork.GetRepositoryInstance<Category>().GetListParameter(x => x.CategoryName != null && x.CategoryName.ToLower().Contains(search.ToLower())).ToPagedList(page ?? 1, 5);


            //}
            //else
            //{
            //    model.ListOfCategory = repoWork.GetRepositoryInstance<Category>().GetAllRecords().ToPagedList(page ?? 1,5);


            //}
            //return View(model);


            //return View(model.CreateModel(search, 5, page));

            //CategoryViewModels model = new CategoryViewModels();
             pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            //if(!string.IsNullOrEmpty(search))
            //{ 
            var totalRec = repoWork.GetRepositoryInstance<Category>().GetCountByWhere(search, x => x.CategoryName != null &&
                         x.CategoryName.ToLower().Contains(search.ToLower()));
            //}
            //else
            //{
            //    var totalRec = repoWork.GetRepositoryInstance<Category>().GetAllrecordCount();
            //}
            model.category = repoWork.GetRepositoryInstance<Category>().GetToShow(search, pageNO.Value, pageSize, x => x.CategoryName != null &&
                         x.CategoryName.ToLower().Contains(search.ToLower()), x => x.CategoryID, x =>x.Products);

              if (model.category != null)
               {
                model.Pager = new Pager(totalRec, pageNO, 5);

                return PartialView("Categories", model);
            }
            else
            {
                return HttpNotFound();
            }
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
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
        
    }
}