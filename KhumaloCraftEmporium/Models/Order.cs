using System;
using System.Collections.Generic;
using KhumaloCraftEmporium.Models; // Ensure this line is present

namespace KhumaloCraftEmporium.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
