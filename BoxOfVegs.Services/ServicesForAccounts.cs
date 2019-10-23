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
        public void ResetPassword(string password, int userid)
        {

            User user = new User();
            user.Password = password;
            user.UserID = userid;
            using (var context = new BOVContext())
            {
                context.Configuration.ValidateOnSaveEnabled = false;
                context.Users.Attach(user);
                context.Entry(user).Property(x=>x.Password).IsModified = true;
                context.SaveChanges();
            }
        }
        public User GetUserDetail(string username)
        {
            using (var context = new BOVContext())
            {
                return context.Users.Where(x => x.UserName == username).FirstOrDefault();
            }
        }
        public void UpdateProfile(User user)
        {
            using (var context = new BOVContext())
            {
                context.Configuration.ValidateOnSaveEnabled = false;
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void RemoveAccount(User user)
        {
            using (var context = new BOVContext())
            {
                var User = context.Users.Find(user.UserID);
                context.Users.Remove(User);
                context.SaveChanges();
            }
        }
    }
}
