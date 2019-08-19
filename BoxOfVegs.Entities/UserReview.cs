using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class UserReview
    {
        public int UserReviewID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Review { get; set; }
        public float Rating { get; set; }
        public DateTime PostDate { get; set; }
        public virtual User User { get; set; }

    }
}
