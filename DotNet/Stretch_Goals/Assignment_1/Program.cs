namespace Assignment_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Rectangle rec1 = new Rectangle();
            rec1.input();
            rec1.Area();

            Rectangle rec2 = new Rectangle();
            rec2.input();
            rec2.Area();

            Console.WriteLine();

            // Using operator overloading
            Rectangle rec3 = rec1 + rec2;

            // Using interface methods
            Console.WriteLine("=== Using Interface Methods ===");
            IShape shape1 = rec1;  // Polymorphism - treating Rectangle as IShape
            IShape shape2 = rec2;

            Console.WriteLine("Shape 1 Area: " + shape1.CalculateArea());
            Console.WriteLine("Shape 2 Area: " + shape2.CalculateArea());

            shape1.DisplayInfo();
            shape2.DisplayInfo();

            Console.WriteLine();
            Console.WriteLine("Sum of areas of 2 rectangles is: " + rec3.area);

            // Demonstrating interface benefits
            Console.WriteLine("\n=== Interface Benefits ===");
            ProcessShape(rec1);
            ProcessShape(rec2);
        }

        // Method that works with any IShape implementation
        static void ProcessShape(IShape shape)
        {
            Console.WriteLine($"Processing shape with area: {shape.CalculateArea()}");
            shape.DisplayInfo();
        }
    }
}