namespace KhumaloCraftEmporium.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation property
        public Order Order { get; set; } // Add this navigation property

        // Navigation property to Product
        public Product Product { get; set; }
    }
}
