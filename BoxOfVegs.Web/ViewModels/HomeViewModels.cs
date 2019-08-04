using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
    public class HomeViewModels
    {
        
        
        public RepositoryWork repoWork = new RepositoryWork();
        BOVContext context = new BOVContext();


        public IEnumerable<Category> Categories { get; set; }
        public IPagedList<Product> ListOfProducts { get; set; }
        public HomeViewModels CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<Product> data = context.Database.SqlQuery<Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
           // IEnumerable<Category> data = repoWork.GetRepositoryInstance<Category>().GetAllRecords();
            return new HomeViewModels
            {
                ListOfProducts = data
            };
        }
        
    }
}