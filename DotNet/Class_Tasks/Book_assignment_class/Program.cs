using System;

namespace Book_assignment_class
{
    // Book class to represent a book entity
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Book(string title, string author, decimal price, int quantity)
        {
            Title = title;
            Author = author;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Title: {Title}\nAuthor: {Author}\nPrice: {Price:F2} rupees\nQuantity: {Quantity}";
        }
    }

    // Sale class to represent a sales transaction
    public class Sale
    {
        public string CustomerName { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public Sale(string customerName, string bookTitle, int quantity, decimal amount)
        {
            CustomerName = customerName;
            BookTitle = bookTitle;
            Quantity = quantity;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"Customer Name: {CustomerName}\nBook Sold: {BookTitle}\nQuantity Purchased: {Quantity}\nAmount: {Amount:F2} rupees";
        }
    }

    // BookShop class to manage the book shop operations
    public class BookShop
    {
        private Book currentBook;
        private Sale lastSale;

        public void AddOrUpdateBook(string title, string author, decimal price, int quantity)
        {
            currentBook = new Book(title, author, price, quantity);
        }

        public bool SellBook(string customerName, int quantity)
        {
            if (currentBook == null || currentBook.Quantity == 0)
            {
                return false;
            }

            if (quantity <= 0 || quantity > currentBook.Quantity)
            {
                return false;
            }

            decimal amount = currentBook.Price * quantity;
            currentBook.Quantity -= quantity;

            lastSale = new Sale(customerName, currentBook.Title, quantity, amount);
            return true;
        }

        public string ViewBook()
        {
            if (currentBook == null)
            {
                return "No book in inventory. Please add a book first.";
            }

            return currentBook.ToString();
        }

        public string ViewSales()
        {
            if (lastSale == null)
            {
                return "No sales recorded yet.";
            }

            return lastSale.ToString();
        }

        public bool HasBook()
        {
            return currentBook != null;
        }

        public int GetBookQuantity()
        {
            return currentBook?.Quantity ?? 0;
        }

        public string GetBookTitle()
        {
            return currentBook?.Title ?? "";
        }
    }

    // Main program class
    internal class Program
    {
        static BookShop bookShop = new BookShop();

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
            bookShop.AddOrUpdateBook(title, author, price, quantity);
            Console.WriteLine("Book details updated successfully.");
        }

        static void SellBook()
        {
            Console.Clear();
            Console.WriteLine("=== SELL BOOK ===");

            if (!bookShop.HasBook() || bookShop.GetBookQuantity() == 0)
            {
                Console.WriteLine("No book available for sale. Please add a book first.");
                return;
            }

            Console.WriteLine("Current book: " + bookShop.GetBookTitle() + " (Available: " + bookShop.GetBookQuantity() + ")");
            Console.Write("Enter quantity to sell: ");
            string quantityInput = Console.ReadLine();
            int quantity;

            if (!int.TryParse(quantityInput, out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity! Please enter a positive number.");
                return;
            }

            if (quantity > bookShop.GetBookQuantity())
            {
                Console.WriteLine("Insufficient stock! Only " + bookShop.GetBookQuantity() + " copies available.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Customer name cannot be empty!");
                return;
            }

            // Process sale
            bool success = bookShop.SellBook(customerName, quantity);

            if (success)
            {
                Console.WriteLine("Sold " + quantity + " copies of '" + bookShop.GetBookTitle() + "' to " + customerName + ".");
            }
            else
            {
                Console.WriteLine("Sale failed. Please try again.");
            }
        }

        static void ViewBook()
        {
            Console.Clear();
            Console.WriteLine("--- Book Details ---");
            Console.WriteLine(bookShop.ViewBook());
        }

        static void ViewSales()
        {
            Console.Clear();
            Console.WriteLine("--- Sales Information ---");
            Console.WriteLine(bookShop.ViewSales());
        }
    }
}