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
    public class ServicesForCategories
    {

        public void AddCategory(Category category)
        {
            using (var context = new BOVContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public Category GetCategory(int categoryId)
        {
            using (var context = new BOVContext())
            {
                return context.Categories.Find(categoryId);
            }
        }
        public List<Category> AllCategories()
        {
            using (var context = new BOVContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetCategoriesList(string search, int pageNO)
        {
            int pageSize = 5;

            using (var context = new BOVContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(x => x.CategoryName != null &&
                         x.CategoryName.ToLower().Contains(search.ToLower()))
                         .OrderBy(x => x.CategoryID)
                         .Skip((pageNO - 1) * pageSize)
                         .Take(pageSize)
                         .Include(x => x.Products)
                         .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.CategoryID)
                        .Skip((pageNO - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
            }
        }

        public int GetCountOfCategories(string search)
        {
            using (var context = new BOVContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(x => x.CategoryName != null &&
                         x.CategoryName.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        public List<Category> GetFeaturedCategory()
        {
            using (var context = new BOVContext())
            {
                return context.Categories.Where(x => x.isFeatured /*&& x.ImageURL != null*/).Include(x => x.Products).ToList();
            }
        }

        

        public void UpdateCategory(Category category)
        {
            using (var context = new BOVContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategoryWithRange(int categoryId)
        {
            using (var context = new BOVContext())
            {
                var category = context.Categories.Where(x => x.CategoryID == categoryId).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(category.Products); //first delete products of this category
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
