using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Services
{
    public class ServicesForAccounts
    {
        public void RegisterUser(User user)
        {
            using (var context = new BOVContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        //public void CheckUserDetail(User user)
        //{
        //    using (var context = new BOVContext())
        //    {
        //        var users = context.Users.Single(x => x.UserName && x.Password == user.Password);
                
        //    }
        //}
        //public void Login(User user)
        //{
        //    using (var context=new BOVContext())
        //    {
        //        var users = context.Users.Single(x => x.UserName == user.UserName && x.Password==user.Password);
        //        if (users != null)
        //        {
        //            Session["UserID"] = users.UserID.ToString();
        //        }
        //    }

        //}
    }
}
