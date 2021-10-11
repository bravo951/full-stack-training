using System;
using System.Collections.Generic;
using System.Text;

namespace CartApp.Data.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discount { get; set; }
    }
}
