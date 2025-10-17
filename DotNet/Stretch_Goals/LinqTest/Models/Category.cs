using LinqTest.Models;

namespace LinqTest.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<Product> Products { get; set; }
    }
}