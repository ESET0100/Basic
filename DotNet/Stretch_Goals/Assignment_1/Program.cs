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
            Rectangle rec3=rec1.Areasum(rec1.area, rec2.area);
            rec1.display();
            rec2.display();
            Console.WriteLine();
            Console.WriteLine("Sum of areas of 2 rectangles is: " + rec3.area);
        }
    }
}
