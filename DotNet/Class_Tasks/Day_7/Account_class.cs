using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7
{
    internal class Account
    {
        public string AccountNumber { get; set; }
        public string HolderName { get; set; }
        public double Balance { get; set; }

        public Account(string accountNumber, string holderName, double balance = 0.0)
        {
            AccountNumber = accountNumber;
            HolderName = holderName;
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited ₹{amount}. New Balance: ₹{Balance}");
        }

        public void Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew ₹{amount}. New Balance: ₹{Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Account Balance: ₹{Balance}");
        }
    }
}