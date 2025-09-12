namespace Day1_q4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            string input;

            Console.WriteLine("Enter integers (type 'done' to finish):");
            while (true)
            {
                input = Console.ReadLine();
                if (input.ToLower() == "done")
                {
                    break;
                }

                if (int.TryParse(input, out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer or 'done' to finish.");
                }
            }

            if (numbers.Count > 0)
            {
                int sum = 0;
                int highest = numbers[0];
                int lowest = numbers[0];

                foreach (int num in numbers)
                {
                    sum += num;
                    if (num > highest) highest = num;
                    if (num < lowest) lowest = num;
                }

                double average = (double)sum / numbers.Count;

                Console.WriteLine($"Sum: {sum}");
                Console.WriteLine($"Average: {average}");
                Console.WriteLine($"Highest: {highest}");
                Console.WriteLine($"Lowest: {lowest}");
            }
            else
            {
                Console.WriteLine("No valid numbers were entered.");
            }
        }
    }
}
