using LinqTest.Data;
using LinqTest.Models;
using LinqTest.Data;
using Microsoft.EntityFrameworkCore;

namespace LinqTest.Services
{
    public class LinqQueryService
    {
        private readonly AppDbContext _context;

        public LinqQueryService(AppDbContext context)
        {
            _context = context;
        }

        public void TestAllQueries()
        {
            TestBasicFiltering();
            TestLikeOperations();
            TestInNotInOperations();
            TestGroupByHaving();
            TestJoins();
            TestSubqueries();
            TestAggregation();
            TestOrderingPaging();
            TestComplexQueries();
        }

        public void TestBasicFiltering()
        {
            Console.WriteLine("1. BASIC FILTERING QUERIES");
            Console.WriteLine("===========================");

            // WHERE clause
            var expensiveProducts = _context.Products
                .Where(p => p.Price > 100)
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n📦 Products with price > $100:");
            expensiveProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));

            // Multiple AND conditions
            var activeInStockProducts = _context.Products
                .Where(p => p.IsActive && p.StockQuantity > 0)
                .Select(p => new { p.ProductName, p.Price, p.StockQuantity })
                .ToList();
            Console.WriteLine("\n✅ Active products in stock:");
            activeInStockProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price} (Stock: {p.StockQuantity})"));

            // OR conditions
            var specificPriceProducts = _context.Products
                .Where(p => p.Price < 30 || p.Price > 500)
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n💰 Products with price < $30 OR > $500:");
            specificPriceProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));
        }

        public void TestLikeOperations()
        {
            Console.WriteLine("\n\n2. LIKE OPERATIONS");
            Console.WriteLine("==================");

            // LIKE '%Book%' - Contains
            var bookProducts = _context.Products
                .Where(p => p.ProductName.Contains("Book"))
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n📚 Products containing 'Book':");
            bookProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));

            // LIKE 'Lap%' - StartsWith
            var lapProducts = _context.Products
                .Where(p => p.ProductName.StartsWith("Lap"))
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n💻 Products starting with 'Lap':");
            lapProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));

            // LIKE '%phone' - EndsWith
            var phoneProducts = _context.Products
                .Where(p => p.ProductName.EndsWith("phone"))
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n📱 Products ending with 'phone':");
            phoneProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));

            // NOT LIKE - !Contains
            var nonElectronicProducts = _context.Products
                .Where(p => !p.ProductName.Contains("Book") && !p.ProductName.Contains("Lap") && !p.ProductName.Contains("phone"))
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n🚫 Products NOT containing 'Book', 'Lap', or 'phone':");
            nonElectronicProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));
        }

        public void TestInNotInOperations()
        {
            Console.WriteLine("\n\n3. IN / NOT IN OPERATIONS");
            Console.WriteLine("=========================");

            var targetCategories = new[] { "Electronics", "Books" };

            // IN operation
            var productsInCategories = _context.Products
                .Include(p => p.Category)
                .Where(p => targetCategories.Contains(p.Category.CategoryName))
                .Select(p => new { p.ProductName, Category = p.Category.CategoryName, p.Price })
                .ToList();
            Console.WriteLine("\n🔌 Products in Electronics or Books categories (IN):");
            productsInCategories.ForEach(p => Console.WriteLine($"   - {p.ProductName} ({p.Category}): ${p.Price}"));

            // NOT IN operation
            var productsNotInCategories = _context.Products
                .Include(p => p.Category)
                .Where(p => !targetCategories.Contains(p.Category.CategoryName))
                .Select(p => new { p.ProductName, Category = p.Category.CategoryName, p.Price })
                .ToList();
            Console.WriteLine("\n🚫 Products NOT in Electronics or Books categories (NOT IN):");
            productsNotInCategories.ForEach(p => Console.WriteLine($"   - {p.ProductName} ({p.Category}): ${p.Price}"));
        }

        public void TestGroupByHaving()
        {
            Console.WriteLine("\n\n4. GROUP BY WITH HAVING");
            Console.WriteLine("======================");

            // GROUP BY with HAVING (count > 1)
            var categoryStats = _context.Products
                .Include(p => p.Category)
                .GroupBy(p => p.Category.CategoryName)
                .Where(g => g.Count() > 1)  // HAVING clause
                .Select(g => new
                {
                    Category = g.Key,
                    ProductCount = g.Count(),
                    AvgPrice = g.Average(p => p.Price),
                    TotalStock = g.Sum(p => p.StockQuantity)
                })
                .ToList();
            Console.WriteLine("\n📊 Categories with more than 1 product (GROUP BY + HAVING):");
            categoryStats.ForEach(cs => Console.WriteLine($"   - {cs.Category}: {cs.ProductCount} products, Avg: ${cs.AvgPrice:F2}"));

            // Multiple grouping with HAVING
            var customerOrders = _context.Orders
                .GroupBy(o => o.CustomerName)
                .Where(g => g.Sum(o => o.TotalAmount) > 100)  // HAVING total spent > 100
                .Select(g => new
                {
                    Customer = g.Key,
                    TotalOrders = g.Count(),
                    TotalSpent = g.Sum(o => o.TotalAmount)
                })
                .ToList();
            Console.WriteLine("\n👥 Customers who spent more than $100:");
            customerOrders.ForEach(co => Console.WriteLine($"   - {co.Customer}: {co.TotalOrders} orders, Total: ${co.TotalSpent:F2}"));
        }

        public void TestJoins()
        {
            Console.WriteLine("\n\n5. JOIN OPERATIONS");
            Console.WriteLine("==================");

            // INNER JOIN
            var productsWithCategories = _context.Products
                .Join(_context.Categories,
                      p => p.CategoryId,
                      c => c.CategoryId,
                      (p, c) => new { p.ProductName, Category = c.CategoryName, p.Price })
                .ToList();
            Console.WriteLine("\n🔗 INNER JOIN - Products with Categories:");
            productsWithCategories.ForEach(p => Console.WriteLine($"   - {p.ProductName} ({p.Category}): ${p.Price}"));

            // LEFT JOIN using navigation properties
            var categoriesWithProducts = _context.Categories
                .Select(c => new
                {
                    Category = c.CategoryName,
                    Products = c.Products.Select(p => p.ProductName).ToList()
                })
                .ToList();
            Console.WriteLine("\n⬅️ LEFT JOIN - Categories with their Products:");
            foreach (var category in categoriesWithProducts)
            {
                Console.WriteLine($"   - {category.Category}: {(category.Products.Any() ? string.Join(", ", category.Products) : "No products")}");
            }

            // Multiple table JOIN
            var orderDetails = _context.Orders
                .Include(o => o.Product)
                    .ThenInclude(p => p.Category)
                .Select(o => new
                {
                    OrderId = o.OrderId,
                    Customer = o.CustomerName,
                    Product = o.Product.ProductName,
                    Category = o.Product.Category.CategoryName,
                    Quantity = o.Quantity,
                    Total = o.TotalAmount
                })
                .ToList();
            Console.WriteLine("\n🔄 Multiple JOIN - Order Details:");
            orderDetails.ForEach(od => Console.WriteLine($"   - Order #{od.OrderId}: {od.Customer} bought {od.Quantity}x {od.Product} ({od.Category}) for ${od.Total}"));
        }

        public void TestSubqueries()
        {
            Console.WriteLine("\n\n6. SUBQUERIES");
            Console.WriteLine("=============");

            // Subquery in WHERE clause
            var expensiveProducts = _context.Products
                .Where(p => p.Price > _context.Products.Average(p2 => p2.Price))
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n📈 Products with price above average (Subquery in WHERE):");
            expensiveProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));

            // Correlated subquery
            var productsWithAboveAvgCategoryPrice = _context.Products
                .Where(p => p.Price > _context.Products
                    .Where(p2 => p2.CategoryId == p.CategoryId)
                    .Average(p2 => p2.Price))
                .Include(p => p.Category)
                .Select(p => new { p.ProductName, p.Price, p.Category.CategoryName })
                .ToList();
            Console.WriteLine("\n🎯 Products with price above category average (Correlated Subquery):");
            productsWithAboveAvgCategoryPrice.ForEach(p => Console.WriteLine($"   - {p.ProductName} ({p.CategoryName}): ${p.Price}"));

            // Subquery with ANY
            var categoriesWithOrders = _context.Categories
                .Where(c => _context.Products
                    .Where(p => p.CategoryId == c.CategoryId)
                    .Any(p => p.Orders.Any()))
                .Select(c => c.CategoryName)
                .ToList();
            Console.WriteLine("\n🛒 Categories that have products with orders (EXISTS/ANY):");
            categoriesWithOrders.ForEach(c => Console.WriteLine($"   - {c}"));
        }

        public void TestAggregation()
        {
            Console.WriteLine("\n\n7. AGGREGATION OPERATIONS");
            Console.WriteLine("=========================");

            var stats = _context.Products
                .Where(p => p.IsActive)
                .GroupBy(p => 1)  // Group all records
                .Select(g => new
                {
                    TotalProducts = g.Count(),
                    AveragePrice = g.Average(p => p.Price),
                    MaxPrice = g.Max(p => p.Price),
                    MinPrice = g.Min(p => p.Price),
                    TotalStockValue = g.Sum(p => p.Price * p.StockQuantity)
                })
                .First();

            Console.WriteLine("\n📊 Overall Product Statistics:");
            Console.WriteLine($"   - Total Products: {stats.TotalProducts}");
            Console.WriteLine($"   - Average Price: ${stats.AveragePrice:F2}");
            Console.WriteLine($"   - Max Price: ${stats.MaxPrice:F2}");
            Console.WriteLine($"   - Min Price: ${stats.MinPrice:F2}");
            Console.WriteLine($"   - Total Stock Value: ${stats.TotalStockValue:F2}");

            // Count with condition
            var outOfStockCount = _context.Products.Count(p => p.StockQuantity == 0);
            var activeProductsCount = _context.Products.Count(p => p.IsActive);
            Console.WriteLine($"\n📦 Inventory Stats:");
            Console.WriteLine($"   - Out of stock items: {outOfStockCount}");
            Console.WriteLine($"   - Active products: {activeProductsCount}");
        }

        public void TestOrderingPaging()
        {
            Console.WriteLine("\n\n8. ORDERING AND PAGING");
            Console.WriteLine("======================");

            // Top 3 most expensive products
            var top3Expensive = _context.Products
                .OrderByDescending(p => p.Price)
                .Take(3)
                .Select(p => new { p.ProductName, p.Price })
                .ToList();
            Console.WriteLine("\n🏆 Top 3 Most Expensive Products:");
            top3Expensive.ForEach(p => Console.WriteLine($"   - {p.ProductName}: ${p.Price}"));

            // Paging - Second page of 2 items each
            var page2 = _context.Products
                .OrderBy(p => p.ProductName)
                .Skip(2)
                .Take(2)
                .Select(p => p.ProductName)
                .ToList();
            Console.WriteLine("\n📄 Paging - Page 2 (2 items):");
            page2.ForEach(p => Console.WriteLine($"   - {p}"));

            // Multiple ordering
            var orderedProducts = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Category.CategoryName)
                .ThenBy(p => p.Price)
                .Select(p => new { p.ProductName, p.Category.CategoryName, p.Price })
                .ToList();
            Console.WriteLine("\n🔠 Products ordered by Category (desc) then Price (asc):");
            orderedProducts.ForEach(p => Console.WriteLine($"   - {p.ProductName} ({p.CategoryName}): ${p.Price}"));
        }

        public void TestComplexQueries()
        {
            Console.WriteLine("\n\n9. COMPLEX QUERIES");
            Console.WriteLine("==================");

            // Complex query with multiple joins and conditions
            var customerPurchaseSummary = _context.Orders
                .Include(o => o.Product)
                    .ThenInclude(p => p.Category)
                .GroupBy(o => new { o.CustomerName, o.Product.Category.CategoryName })
                .Select(g => new
                {
                    Customer = g.Key.CustomerName,
                    Category = g.Key.CategoryName,
                    TotalSpent = g.Sum(o => o.TotalAmount),
                    OrderCount = g.Count(),
                    AvgOrderValue = g.Average(o => o.TotalAmount)
                })
                .Where(x => x.TotalSpent > 50)
                .OrderByDescending(x => x.TotalSpent)
                .ToList();

            Console.WriteLine("\n👥 Customer Purchase Summary by Category (> $50):");
            foreach (var summary in customerPurchaseSummary)
            {
                Console.WriteLine($"   - {summary.Customer} in {summary.Category}: ${summary.TotalSpent:F2} ({summary.OrderCount} orders)");
            }

            // Products that have never been ordered
            var neverOrderedProducts = _context.Products
                .Where(p => !p.Orders.Any())
                .Select(p => p.ProductName)
                .ToList();
            Console.WriteLine("\n❌ Products that have never been ordered:");
            neverOrderedProducts.ForEach(p => Console.WriteLine($"   - {p}"));
        }
    }
}