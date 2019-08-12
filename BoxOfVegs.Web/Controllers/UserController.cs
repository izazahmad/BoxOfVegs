using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using BoxOfVegs.Services;
using BoxOfVegs.Web.Hashing;
using BoxOfVegs.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BoxOfVegs.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User register)
        {
            ServicesForAccounts services = new ServicesForAccounts();
            var HashPassword = PasswordHashing.CreateHash(register.Password);
            register.Password = HashPassword;
            services.RegisterUser(register);
            ViewBag.Message = register.FirstName + " " + register.LastName + " Successfully Registered.";
            return Redirect("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            using(BOVContext context=new BOVContext())
            {
                
                var users = context.Users.Single(x => x.UserName==user.UserName );
                bool validateUser = PasswordHashing.ValidatePassword( user.Password, users.Password);
                
                if( validateUser==true)
                {
                    Session["UserID"] = users.UserID.ToString();
                    Session["UserName"] = users.UserName.ToString();
                    Session["FirstName"] = users.FirstName.ToString();
                    return RedirectToAction("Checkout","Shop");
                }
                else
                {
                    ModelState.AddModelError("", "Usrname or Password is wrong.");
                }
            }
            return View();

        }
        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Checkout");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
    
}