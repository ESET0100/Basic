using LinqTest.Models;

namespace LinqTest.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation properties
        public Product Product { get; set; }
    }
}