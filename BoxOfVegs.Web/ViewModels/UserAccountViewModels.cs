using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
    public class UserRegisterViewModel
    {
        
        
        [Required(ErrorMessage = "User name is required")]
        [MinLength(3), MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]

        [Compare("Password",ErrorMessage ="password not match with the confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Email address is reqired")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter Valid Email address")]
        public string Email { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        [MinLength(3), MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "password not match with the confirm password")]
        public string NewConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Email address is reqired")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter Valid Email address")]
        public string Email { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "password not match with the confirm password")]
        public string NewConfirmPassword { get; set; }
    }

}