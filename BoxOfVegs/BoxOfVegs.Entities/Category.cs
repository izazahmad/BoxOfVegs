
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage ="Category Name is required")]
        [MinLength(3), MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public List<Product> Products { get; set; }
        
        
    }

}
