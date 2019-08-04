using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{

    public class CategoryPagingModels
    {
        //public RepositoryWork repoWork = new RepositoryWork();
        //BOVContext context = new BOVContext();
        public IEnumerable<Category> categories { get; set; }
        public List<Category> category { get; set; }
        public IPagedList<Category> ListOfCategory { get; set; }
        public string Searching { get; set; }
        public Pager Pager { get; set; }
        //public CategoryPagingModels CreateModel(string search, int size, int? page)
        //{
        //    SqlParameter[] param = new SqlParameter[]{
        //        new SqlParameter("@search",search??(object)DBNull.Value)
        //    };
        //    IPagedList<Category> data = context.Database.SqlQuery<Category>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, size);
        //    IEnumerable<Category> dadt = repoWork.GetRepositoryInstance<Category>().GetResultBySqlprocedure("searchFrom @search", param);
        //    //IEnumerable<Category> data = repoWork.GetRepositoryInstance<Category>().GetAllRecords();
        //    //IEnumerable<Category> data = context.Database.SqlQuery<Category>("GetBySearch @search", param).ToList();
        //    return new CategoryPagingModels
        //    {
        //        ListOfCategory = data
        //    };
        //}
    }
    public class CategoryViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Search { get; set; }
        public Pager Pager { get; set; }
        public List<Category> categories { get; set; }
    }
    public class NewCategoryViewModel
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

      
    }
}