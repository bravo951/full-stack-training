using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public Guid PurchasedNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchasedDateTime { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }

    }
}
