using LinqTest.Models;

namespace LinqTest.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public Category Category { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}