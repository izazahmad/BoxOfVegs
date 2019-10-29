﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
