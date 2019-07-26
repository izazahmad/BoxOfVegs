using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BoxOfVegs.Services
{
    public class ProductService
    {
        public Product GetProduct(int Id)
        {
            using (var context = new BOVContext())
            {
                return context.Products.Find(Id);
            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new BOVContext())
            {
                
                return context.Products.Include(x=>x.Category).ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int Id)
        {
            using (var context = new BOVContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;

                var product = context.Products.Find(Id);

                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        //public List<Product> GetSearchProducts(string search)
        //{
        //    using (var context = new BOVContext())
        //    {
        //        //if (!string.IsNullOrEmpty(search))
        //        //{

        //            //var products = context.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();

        //            return context.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
                
        //    }
        //}
    }
}
