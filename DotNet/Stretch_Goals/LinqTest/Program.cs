using LinqTest.Data;
using LinqTest.Services;
using LinqTest.Data;
using LinqTest.Services;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("🚀 Starting LINQ Demo Application...\n");

            using var context = new AppDbContext();

            // Test database connection
            if (!context.Database.CanConnect())
            {
                Console.WriteLine("❌ Cannot connect to database. Please check your connection string.");
                Console.WriteLine("💡 Make sure:");
                Console.WriteLine("   - SQL Server is running");
                Console.WriteLine("   - Database 'LinqDemoDB' exists");
                Console.WriteLine("   - Connection string is correct");
                return;
            }

            Console.WriteLine("✅ Database connection successful!\n");

            // Initialize the query service
            var queryService = new LinqQueryService(context);

            // Run all LINQ queries
            queryService.TestAllQueries();

            Console.WriteLine("\n🎉 ALL QUERIES COMPLETED SUCCESSFULLY!");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}