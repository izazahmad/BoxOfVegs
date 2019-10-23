using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class User
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="User name is required")]
        [MinLength(3),MaxLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is required")]
        
        public string Password { get; set; }
        [Required(ErrorMessage ="First name is required")]
        
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last name is required")]
        public string LastName { get; set; }
        public int UserRoleID { get; set; }
        [Required(ErrorMessage ="Email address is reqired")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter Valid Email address")]
        public string Email { get; set; }
    }
}
