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

                User user = new User
                {
                    UserRoleID = 2,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    UserName = register.UserName,
                    Password = HashPassword,
                    Email = register.Email
                };
                services.RegisterUser(user);
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            ServicesForAccounts services = new ServicesForAccounts();
            var users = services.GetUserDetail(user.UserName);
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
                        if(Convert.ToInt32(Session["UserRoleID"])==1)
                        {
                           return RedirectToAction("Index", "Category");
                        }
                        else
                        { 
                           return RedirectToAction("Cart", "Shop");
                        }
                }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is wrong.");
                    }
                }
            
            return View();

        }
      
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
            ServicesForAccounts services = new ServicesForAccounts();
            var user = services.GetUserDetail(forgot.UserName);

            if (user != null && user.UserName == forgot.UserName)
            {
                if(user.Email == forgot.Email)
                {
                    if (ModelState.IsValid)
                    {

                        var HashPassword = PasswordHashing.CreateHash(forgot.NewPassword);
                     
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
        public ActionResult ChangePassword()
        {
            if(Session["UserName"]==null)
            {
                return Redirect("Login");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
           

            if (Session["UserName"] != null)
            {
                ServicesForAccounts services = new ServicesForAccounts();
                var user = services.GetUserDetail(Session["UserName"].ToString());
                if (user != null && change.OldPassword != null && change.NewPassword != null)
                {
                    bool ValidatePassword = PasswordHashing.ValidatePassword(change.OldPassword, user.Password);
                    if (ValidatePassword == true)
                    {
                        
                            var HashPassword = PasswordHashing.CreateHash(change.NewPassword);

                            services.ResetPassword(HashPassword, user.UserID);
                            ModelState.Clear();
                            ViewBag.message = "Password has been successfully changed";
                            return View("ChangePassword");

                       

                    }
                    else
                    {
                        ModelState.AddModelError("", "Old password is wrong");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Old password is wrong");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult ProFile(string username)
        {
            if (Session["UserRoleID"] == null)
            {

                return RedirectToAction("Login", "User");


            }
            else {
                ServicesForAccounts services = new ServicesForAccounts();
                username = Session["UserName"].ToString();
                var user = services.GetUserDetail(username);
                return View(user);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProFile(User user)
        {
            if(Session["UserRoleID"]==null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ServicesForAccounts services = new ServicesForAccounts();
                user.UserName = Session["UserName"].ToString();
                services.UpdateProfile(user);
                return View(user);
            }

        }
        public ActionResult RemoveAccount()
        {
            if (Session["UserName"] == null)
            {
                return Redirect("Login");
            }
            else
            {
                ServicesForAccounts services = new ServicesForAccounts();
                var user = services.GetUserDetail(Session["UserName"].ToString());
                return View(user);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAccount(User user)
        {
            if(Session["UserName"] == null)
            {
                return Redirect("Login");
            }
            else
            {
                ServicesForAccounts services = new ServicesForAccounts();
                services.RemoveAccount(user);
                Session.Abandon();
                FormsAuthentication.SignOut();
                return Redirect("Login");
            }
        }

    }
    
}