using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxOfVegs.Web.ViewModels
{
	public class InvoiceDetailViewModel
	{
		public List<Order> Orders { get; set; }
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int InvoiceID { get; set; }
		public string Address { get; set; }
		public string PostCode { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime InvoiceDate { get; set; }
		public decimal TotalAmount { get; set; }
		
	}
}