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
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterViewModel register)
        {
            BOVContext context = new BOVContext();
            if(context.Users.Any(u=>u.UserName==register.UserName))
            {
                ModelState.AddModelError("", "User name is already exist choose different.");

            }
            if (ModelState.IsValid)
            {
                ServicesForAccounts services = new ServicesForAccounts();
                var HashPassword = PasswordHashing.CreateHash(register.Password);
                //register.Password = HashPassword;
                //register.UserRoleID = 2;
                User user = new User();
                user.UserRoleID = 2;
                user.FirstName = register.FirstName;
                user.LastName = register.LastName;
                user.UserName = register.UserName;
                user.Password = HashPassword;
                user.Email = register.Email;
                services.RegisterUser(user);
                //ViewBag.Message = register.FirstName + " " + register.LastName + " Successfully Registered.";
                return Redirect("Login");

            }
            else
            {
                ModelState.AddModelError("","");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            //BOVContext context = new BOVContext();
            ServicesForAccounts services = new ServicesForAccounts();
            var users = services.GetUserDetail(user.UserName);
                //var users = context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
                if (users == null)
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
                else
                {
                    bool validateUser = PasswordHashing.ValidatePassword(user.Password, users.Password);

                    if (validateUser == true)
                    {
                        Session["UserID"] = users.UserID.ToString();
                        Session["UserName"] = users.UserName.ToString();
                        Session["FirstName"] = users.FirstName.ToString();
                        Session["UserRoleID"] = users.UserRoleID.ToString();
                        return RedirectToAction("Cart", "Shop");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is wrong.");
                    }
                }
            
            return View();

        }
        //public ActionResult LoggedIn()
        //{
        //    if (Session["UserID"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Checkout");
        //    }
        //}
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            //BOVContext context = new BOVContext();
            ServicesForAccounts services = new ServicesForAccounts();
            var user = services.GetUserDetail(forgot.UserName);
            //var user = context.Users.Where(x => x.UserName == forgot.UserName).FirstOrDefault();

            if (/*context.Users.Any(u => u.UserName == forgot.UserName)*/user != null && user.UserName == forgot.UserName)
            {
                if(/*context.Users.Any(u=>u.Email==forgot.Email)*/user.Email == forgot.Email)
                {
                    if (ModelState.IsValid)
                    {
                        //var users = context.Users.Single(x => x.UserName == forgot.UserName);

                        var HashPassword = PasswordHashing.CreateHash(forgot.NewPassword);
                        //User user = new User();
                        //user.Password = HashPassword;
                        //user.UserName=forgot.UserName;
                        services.ResetPassword(HashPassword, user.UserID);
                        return Redirect("Login");

                    }
                    else
                    {
                        ModelState.AddModelError("", "");

                    }

                }
                else
                {
                    ModelState.AddModelError("", "Email address is not existed");

                }
            }
            else
            {
                ModelState.AddModelError("", "User name is not existed");

            }
            return View();
        }

    }
    
}