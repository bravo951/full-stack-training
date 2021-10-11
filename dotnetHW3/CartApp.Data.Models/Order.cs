using System;

namespace CartApp.Data.Models
{
    public class Order
    {
        
        public int Id { get; set; }
        public int AppleNo { get; set; }
        public int OrangeNo { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderDate { get; set; }
        
    }
}
