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
    public class ServicesForProducts
    {
        
        public void AddProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public int GetCountOfProducts(string search)
        {
            using (var context = new BOVContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.ProductName != null &&
                         product.ProductName.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.Products.Count();
                }
            }
        }
        public List<Product> GetProductsList(string search, int pageNo)
        {
            int pageSize =5;
            using (var context = new BOVContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.ProductName != null &&
                         product.ProductName.ToLower().Contains(search.ToLower()))
                         .OrderBy(x => x.ProductID)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                         .Include(x => x.Category)
                         .ToList();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.ProductID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }

        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new BOVContext())
            {
                return context.Products.OrderByDescending(x => x.ProductID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public Product GetProduct(int productID)
        {
            using(var context=new BOVContext())
            {
                return context.Products.Where(x => x.ProductID == productID).Include(x => x.Category).FirstOrDefault();
            }
        }

        //public List<Product> GetFeaturedProducts()
        //{
        //    using (var context = new BOVContext())
        //    {
        //        return context.Products.Where(x => x.isFeatured /*&& x.ImageURL != null*/).ToList();
        //    }
        //}

        public void UpdateProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void RemoveProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                var Product = context.Products.Find(product.ProductID);

                context.Products.Remove(Product);
                context.SaveChanges();
            }
        }
        public List<Product> GetNewAddedProducts(int numberToDidplay)
        {
            using (var context = new BOVContext())
            {
                return context.Products.OrderByDescending(x => x.ProductID).Take(numberToDidplay).Include(x => x.Category).ToList();
            }
        }
        public int GetMaxPrice()
        {
            using (var context = new BOVContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }
        public int ShopProductsCount(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy)
        {
            using (var context = new BOVContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.CategoryID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    products = products.Where(x => x.ProductName.ToLower().Contains(search.ToLower())).ToList();
                }

                if (minPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minPrice.Value).ToList();
                }

                if (maxPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {

                    if (sortBy.Value == 1)
                    {
                        products = products.OrderByDescending(x => x.ProductID).ToList();
                    }
                    if (sortBy.Value == 2)
                    {
                        products = products.OrderBy(x => x.Price).ToList();
                    }
                    if (sortBy.Value == 3)
                    {
                        products = products.OrderByDescending(x => x.Price).ToList();
                    }
                    
                    
                }

                return products.Count;
            }
        }
        public List<Product> GetProductByCatName(string categoryName)
        {
            using (var context = new BOVContext())
            {
                //return context.Products.Where(x => x.Category.CategoryName== categoryName).ToList();
                return context.Products.Where(x => x.ProductName.ToLower().Contains(categoryName.ToLower())).Include(x => x.Category).ToList();

            }
        }
        public List<Product> ShopProducts(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy/*, int pageNo, int pageSize*/)
        {
            using (var context = new BOVContext())
            {
                var products = context.Products.Include(x => x.Category).ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.CategoryID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    products = products.Where(x => x.ProductName.ToLower().Contains(search.ToLower())).ToList();
                    
                }

                if (minPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minPrice.Value).ToList();
                }

                if (maxPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {

                    if (sortBy.Value == 1)
                    {
                        products = products.OrderByDescending(x => x.ProductID).ToList();
                    }
                    if (sortBy.Value == 2)
                    {
                        products = products.OrderBy(x => x.Price).ToList();
                    }
                    if (sortBy.Value == 3)
                    {
                        products = products.OrderByDescending(x => x.Price).ToList();
                    }
                    

                    
                }

                return products/*.Skip((pageNo - 1) * pageSize).Take(pageSize)*/.ToList();
            }
        }
        public void AddReview(UserReview review)
        {
            using (var context = new BOVContext())
            {
                context.UserReviews.Add(review);
                context.SaveChanges();
            }
        }
        public List<UserReview> GetProductReview(int productId)
        {
            using (var context = new BOVContext())
            { 
                var review = context.UserReviews.Where(x => x.ProductID == productId).Include(x=>x.User).ToList();
                return review;
            }

        }
        public float GetAverageRating(int? productId)
        {
            using(var context=new BOVContext())
            {

                return context.UserReviews.Where(x => x.ProductID == productId).Average(x => x.Rating);
            }
        }
        public int GetCountUserReview(int productId)
        {
            using(var context=new BOVContext())
            {
                return context.UserReviews.Where(x => x.ProductID == productId).Count();
            }
        }
        public void UpdateQuantity(int productId,int quantity)
        {
            //Product product = new Product();
            //product.ProductID = productId;
            //product.Quantity = quantity;
            using (var context = new BOVContext())
            {
                Product product = context.Products.FirstOrDefault(x => x.ProductID == productId);
                context.Configuration.ValidateOnSaveEnabled = false;
                product.Quantity = quantity;
                //context.Products.Attach(product);
                //context.Entry(product).Property(x => x.Quantity).IsModified = true;
                context.SaveChanges();
                //context.Entry(product).Property(x => x.Quantity) = Modified;
                //context.SaveChanges();
            }
        }
    }
}
