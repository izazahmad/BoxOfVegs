using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Services
{
    public class ServicesForShop
    {
        
        public void AddOrder(Order order)
        {
            using (var context = new BOVContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
        public void AddInvoice(Invoice invoice)
        {
            using (var context = new BOVContext())
            {
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
        }
       
        public List<Order> GetOrders(int invoiceId)
        {
            using (var context = new BOVContext())
            {
                
                return context.Orders.Where(i=>i.InvoiceID==invoiceId).ToList();
            }
        }
       
        public Invoice GetInvoiceDetail(int userId)
        {
            using (var context = new BOVContext())
            {
                return context.Invoices.OrderByDescending(d=>d.InvoiceDate).Where(u => u.UserID == userId).FirstOrDefault();
            }
        }
        public User GetUserDetails(int userId)
        {
            using (var context = new BOVContext())
            {
                return context.Users.Where(x => x.UserID == userId).FirstOrDefault();
            }
        }
        public List<Invoice> GetInvoices(int userID)
        {
            using (var context = new BOVContext())
            {
                return context.Invoices.OrderByDescending(d => d.InvoiceDate).Where(u => u.UserID == userID).ToList();
            }
        }
        //public List<Order> GetOrdersByInvoice(int UserID)
        //{
        //    using (var context = new BOVContext())
        //    {
        //        //List<Order> orders = context.Orders.ToList();
        //        //List<Invoice> invoices = context.Invoices.ToList();
        //        //var orderRecords = from o in orders
        //        //                    join i in invoices
        //        //                    on o.InvoiceID equals i.InvoiceID

        //        //                    where i.UserID == UserID
        //        //                    select new
        //        //                    {
        //        //                        o.OrderID,
        //        //                        o.ProductID,
        //        //                        o.ProductName,
        //        //                        o.Quantity,
        //        //                        o.Product,
        //        //                        o.Subtotal,
        //        //                        o.UnitPrice,
        //        //                        i.InvoiceDate,
        //        //                        i.UserID,
        //        //                        i.User,
        //        //                        i.PostCode,
        //        //                        i.Address,
        //        //                        i.City
        //        //                    };
        //        var orderRecords = context.Orders.Join(context.Invoices, o => o.InvoiceID, i => i.InvoiceID, (o, i) => new { o, i })
        //                                        .Where(u => u.i.UserID == UserID)
        //                                        .Select(u => new OrdersVeiwModel
        //                                        {
        //                                            orderId = u.o.OrderID,
        //                                            productId = u.o.ProductID,
        //                                            productName = u.o.ProductName,
        //                                            quantity = u.o.Quantity,

        //                                            subtotal = u.o.Subtotal,
        //                                            unitPrice = u.o.UnitPrice,
        //                                            invoiceDate = u.i.InvoiceDate,
        //                                            userID = u.i.UserID,

        //                                            postCode = u.i.PostCode,
        //                                            address = u.i.Address,
        //                                            city = u.i.City
        //                                        });
        //        //var orderRecords=context.Orders.Include(i=>i.Invoice).Include()
        //        return orderRecords.ToList();
        //    }
        //}
       
        
    }
}
