using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
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
    }
}
