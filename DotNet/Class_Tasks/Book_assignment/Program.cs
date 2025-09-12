using System;
namespace Book_assignment
{
    internal class Program
    {
        static string bookTitle = "";
        static string bookAuthor = "";
        static decimal bookPrice = 0;
        static int bookQuantity = 0;

        static string saleCustomer = "";
        static string saleBook = "";
        static int saleQuantity = 0;
        static decimal saleAmount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Simple Book Shop Management System!");

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        SellBook();
                        break;
                    case "3":
                        ViewBook();
                        break;
                    case "4":
                        ViewSales();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Thanks for using Book Shop Management!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("====== SIMPLE BOOK SHOP MENU ======");
            Console.WriteLine("1. Add/Update Book");
            Console.WriteLine("2. Sell Book");
            Console.WriteLine("3. View Book");
            Console.WriteLine("4. View Sales");
            Console.WriteLine("5. Exit");
            Console.WriteLine("===================================");
            Console.Write("Choice: ");
        }

        static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== ADD/UPDATE BOOK ===");

            Console.Write("Enter book title: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty!");
                return;
            }

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Author cannot be empty!");
                return;
            }

            Console.Write("Enter price: ");
            string priceInput = Console.ReadLine();
            decimal price;

            if (!decimal.TryParse(priceInput, out price) || price <= 0)
            {
                Console.WriteLine("Invalid price! Please enter a positive number.");
                return;
            }

            Console.Write("Enter quantity: ");
            string quantityInput = Console.ReadLine();
            int quantity;

            if (!int.TryParse(quantityInput, out quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity! Please enter a non-negative number.");
                return;
            }

            // Update book details
            bookTitle = title;
            bookAuthor = author;
            bookPrice = price;
            bookQuantity = quantity;

            Console.WriteLine("Book details updated successfully.");
        }

        static void SellBook()
        {
            Console.Clear();
            Console.WriteLine("=== SELL BOOK ===");

            if (bookQuantity == 0 || string.IsNullOrEmpty(bookTitle))
            {
                Console.WriteLine("No book available for sale. Please add a book first.");
                return;
            }

            Console.WriteLine("Current book: " + bookTitle + " (Available: " + bookQuantity + ")");
            Console.Write("Enter quantity to sell: ");
            string quantityInput = Console.ReadLine();
            int quantity;

            if (!int.TryParse(quantityInput, out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity! Please enter a positive number.");
                return;
            }

            if (quantity > bookQuantity)
            {
                Console.WriteLine("Insufficient stock! Only " + bookQuantity + " copies available.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Customer name cannot be empty!");
                return;
            }

            // Update book quantity
            bookQuantity -= quantity;

            // Record sale
            decimal amount = bookPrice * quantity;
            saleCustomer = customerName;
            saleBook = bookTitle;
            saleQuantity = quantity;
            saleAmount = amount;

            Console.WriteLine("Sold " + quantity + " copies of '" + bookTitle + "' to " + customerName + ".");
        }

        static void ViewBook()
        {
            Console.Clear();
            Console.WriteLine("--- Book Details ---");

            if (string.IsNullOrEmpty(bookTitle))
            {
                Console.WriteLine("No book in inventory. Please add a book first.");
                return;
            }

            Console.WriteLine("Title: " + bookTitle);
            Console.WriteLine("Author: " + bookAuthor);
            Console.WriteLine("Price: " + bookPrice.ToString("F2") + " rupees");
            Console.WriteLine("Quantity: " + bookQuantity);
        }

        static void ViewSales()
        {
            Console.Clear();
            Console.WriteLine("--- Sales Information ---");

            if (string.IsNullOrEmpty(saleCustomer))
            {
                Console.WriteLine("No sales recorded yet.");
                return;
            }

            Console.WriteLine("Customer Name: " + saleCustomer);
            Console.WriteLine("Book Sold: " + saleBook);
            Console.WriteLine("Quantity Purchased: " + saleQuantity);
            Console.WriteLine("Amount: " + saleAmount.ToString("F2") + " rupees");
        }
    }
}