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
        
    }
}
