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
        //public int ShopPageSize()
        //{
        //    using (var context = new BOVContext())
        //    {
        //        var pageSizeConfig = context.Configurations.Find("ShopPageSize");

        //        return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
        //    }
        //}
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
        //public List<User> GetInvoiceSqlprocedure(int userId)
        //{
        //    using (var context = new BOVContext())
        //    {
        //        int invoiceId =Convert.ToInt32(context.Invoices.OrderByDescending(u => u.UserID == userId).FirstOrDefault());
        //        SqlParameter param = new SqlParameter("@InvoiceID", invoiceId);
        //        return context.Database.SqlQuery<User>("InvoiceDetails",param).ToList();
                
                    
                
        //    }
        //}
        public List<Order> GetOrders(int invoiceId)
        {
            using (var context = new BOVContext())
            {
                
                return context.Orders.Where(i=>i.InvoiceID==invoiceId).ToList();
            }
        }
        //public List<int> GetInvoiceID(int userId)
        //{
        //    using (var context = new BOVContext())
        //    {
        //        var invoiceid = context.Invoices.Where(i=>i.UserID==userId).Select(i => i.InvoiceID).Distinct().ToList();
        //        //int invoice = Convert.ToInt32(invoiceid);
        //        return invoiceid;
        //    }
        //}
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
        
    }
}
