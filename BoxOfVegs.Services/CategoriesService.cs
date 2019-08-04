using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Services
{
    public class CategoriesService
    {

        public Category GetCategory(int Id)
        {
            using (var context = new BOVContext())
            {
                return context.Categories.Find(Id);
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new BOVContext())
            {
                return context.Categories.ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            using (var context = new BOVContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new BOVContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        //public void DeleteCategory(int Id)
        //{
        //    using (var context = new BOVContext())
        //    {
        //        //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;

        //        var category = context.Categories.Find(Id);

        //        context.Categories.Remove(category);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteCategory(int ID)
        {
            using (var context = new BOVContext())
            {
                var category = context.Categories.Where(x => x.CategoryID == ID).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(category.Products); //first delete products of this category
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
