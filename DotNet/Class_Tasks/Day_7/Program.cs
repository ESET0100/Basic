using Day_7;

namespace Day_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount acc = new SavingsAccount("SB001", "Amit Kumar", 1000.0);
            acc.DisplayBalance();
            acc.Deposit(500);
            acc.Withdraw(200);
            acc.ApplyInterest();

            Console.ReadLine();
        }
    }
}
