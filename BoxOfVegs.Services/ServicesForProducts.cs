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
        // methods for adding product in database in products table
        public void AddProduct(Product product)
        {
            //its call the BOVContext class to open connection to database
            using (var context = new BOVContext())
            {
                //entity framwork linq query to store product in table
                context.Products.Add(product);
                //it saves any changes of the table
                context.SaveChanges();
            }
        }
        //method for getting count of the products
        public int GetCountOfProducts(string search)
        {
            //its call the BOVContext class to open connection to database
            using (var context = new BOVContext())
            {
                //if statement checks if search string is not empty
                if (!string.IsNullOrEmpty(search))
                {
                    //return the count where product name is not null and product name contains the search string
                    return context.Products.Where(product => product.ProductName != null &&
                         product.ProductName.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    //if search string is empty than it return the all products count
                    return context.Products.Count();
                }
            }
        }
        //method to get products list
        public List<Product> GetProductsList(string search, int pageNo)
        {
            //variable for pagesize which is used to show only 5 entries in a page
            int pageSize =5;
            //its call the BOVContext class to open connection to database
            using (var context = new BOVContext())
            {
                //if statement checks if search string is not empty

                if (!string.IsNullOrEmpty(search))
                {
                    ///return the list of products where product name is not null and product name contains the search string
                    ///it arranges product list in ascending order by sorting productID
                    ///it skips the record like if page no is 3 then (3-1)*5=10 means 10 records it skips
                    ///take shows the only five records on a page
                    ///includes fetch the category detail of the perticular products
                     
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
                    ///if search string is empty than it returns the all product list
                    ///it arranges product list in ascending order by sorting productID
                    ///it skips the record like if page no is 3 then (3-1)*5=10 means 10 records it skips
                    ///take shows the only five records on a page
                    ///includes fetch the category detail of the perticular products
                    return context.Products
                        .OrderBy(x => x.ProductID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }
        //methods to get products list in descending order
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            //if statement checks if search string is not empty
            using (var context = new BOVContext())
            {
                //return list which is sorted by product id in descending order and it displays according to pagesize and it includes category detail as well
                return context.Products.OrderByDescending(x => x.ProductID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        //method to get the single product details according to product ID
        public Product GetProduct(int productID)
        {
            //if statement checks if search string is not empty
            using (var context=new BOVContext())
            {
                //return the product list by matching product id
                return context.Products.Where(x => x.ProductID == productID).Include(x => x.Category).FirstOrDefault();
            }
        }
        //method to update products details in database records
        public void UpdateProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        //method for delete product record from the database
        public void RemoveProduct(Product product)
        {
            using (var context = new BOVContext())
            {
                //find the records by matching product id and store in Product variable
                var Product = context.Products.Find(product.ProductID);
                //LINQ query to delete record which is store in Product variable from the database
                context.Products.Remove(Product);
                context.SaveChanges();
            }
        }
        //method to get 4 newest added product list
        public List<Product> GetNewAddedProducts(int numberToDidplay)
        {
            using (var context = new BOVContext())
            {
                // return products list. first arrange product list in descending order by sorting product id and return first 4 entry
                return context.Products.OrderByDescending(x => x.ProductID).Take(numberToDidplay).Include(x => x.Category).ToList();
            }
        }
        //method to get maximum price
        public int GetMaxPrice()
        {
            using (var context = new BOVContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }
        //method to get products count for shop page products list
        public int ShopProductsCount(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy)
        {
            using (var context = new BOVContext())
            {
                //store all products list in products variable
                var products = context.Products.ToList();
                //it checks if products list need according category ID
                if (categoryID.HasValue)
                {
                    //store products list which are belong to perticular category in products variable
                    products = products.Where(x => x.Category.CategoryID == categoryID.Value).ToList();
                }
                //it checks if search string is not empty or null
                if (!string.IsNullOrEmpty(search))
                {
                    //store product list by matching search string in products variable
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
                return context.Products.Where(x => x.ProductName.ToLower().Contains(categoryName.ToLower())).Include(x => x.Category).ToList();

            }
        }
        public List<Product> ShopProducts(string search, int? minPrice, int? maxPrice, int? categoryID, int? sortBy)
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

                return products.ToList();
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
            
            using (var context = new BOVContext())
            {
                Product product = context.Products.FirstOrDefault(x => x.ProductID == productId);
                context.Configuration.ValidateOnSaveEnabled = false;
                product.Quantity = quantity;
                context.SaveChanges();
                
            }
        }
    }
}
