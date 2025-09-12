namespace Day1_q2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter meter reading:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int meterReading))
            {
                if (meterReading >= 100 && meterReading <= 200)
                {
                    Console.WriteLine("The meter reading is within the valid range (100-200)");
                }
                else
                {
                    Console.WriteLine("The meter reading is out of the valid range (100-200)");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");

            }
        }
    }
}