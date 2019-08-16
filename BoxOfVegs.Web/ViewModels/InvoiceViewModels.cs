using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[Required(ErrorMessage ="address is required")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Postcode is required")]

		public string PostCode { get; set; }
		[Required(ErrorMessage = "City is required")]

		public string City { get; set; }
		[Required(ErrorMessage = "Phone number is required")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		public DateTime InvoiceDate { get; set; }
		public decimal TotalAmount { get; set; }
		
	}
}