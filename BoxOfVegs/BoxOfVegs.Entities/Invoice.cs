using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int UserID { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

       
        public string PhoneNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
