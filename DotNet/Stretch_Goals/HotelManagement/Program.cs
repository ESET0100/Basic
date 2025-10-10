using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== HOTEL MANAGEMENT SYSTEM ===");
            Console.WriteLine("Initializing Database Connection...\n");

            // Create database connection object
            Database_conn dbConnection = new Database_conn();
            SqlConnection conn = dbConnection.GetConnection();

            try
            {
                // Open the connection
                if (dbConnection.OpenConnection())
                {
                    Console.WriteLine("Database connected successfully!\n");

                // Check what tables exist in the database
                    dbConnection.CheckTables();

                    // Handle login authentication
                    if (HandleLogin(conn))
                    {
                        // Run the menu-driven program only if login is successful
                        RunMenu(conn);
                    }
                    else
                    {
                        Console.WriteLine("Access denied. Exiting the system...");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to connect to database. Exiting...");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                // Close the connection
                dbConnection.CloseConnection();
            }
        }

        // Login system
        static bool HandleLogin(SqlConnection conn)
        {
            Console.WriteLine("\n=== STAFF LOGIN SYSTEM ===");
            Console.WriteLine("Welcome to Hotel Management System!");
            Console.WriteLine("Only authorized staff can access the database.\n");

            while (true)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Existing User - Login");
                Console.WriteLine("2. New User - Register");
                Console.WriteLine("3. Exit System");
                Console.Write("Enter your choice (1-3): ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            if (HandleExistingUserLogin(conn))
                            {
                                Console.WriteLine("\nLogin successful! Welcome to the system.");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("\nLogin failed! Invalid credentials.");
                                Console.WriteLine("Please try again or register as a new user.\n");
                            }
                            break;

                        case 2:
                            if (HandleNewUserRegistration(conn))
                            {
                                Console.WriteLine("\nRegistration successful! You are now logged in.");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("\nRegistration failed! Please try again.\n");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Exiting the system...");
                            return false;

                        default:
                            Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.\n");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n");
                }
            }
        }

        static bool HandleExistingUserLogin(SqlConnection conn)
        {
            Console.WriteLine("\n--- EXISTING USER LOGIN ---");
            
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (!Login.ValidateCredentials(username, password))
            {
                return false;
            }

            return Login.AuthenticateUser(conn, username, password);
        }

        static bool HandleNewUserRegistration(SqlConnection conn)
        {
            Console.WriteLine("\n--- NEW USER REGISTRATION ---");
            
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            
            Console.Write("Enter Staff Name: ");
            string staffName = Console.ReadLine();
            
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (!Login.ValidateCredentials(username, password))
            {
                return false;
            }

            if (string.IsNullOrEmpty(staffName))
            {
                Console.WriteLine("Staff name cannot be empty!");
                return false;
            }

            Console.WriteLine("Confirming registration...");
            return Login.RegisterNewUser(conn, username, staffName, password);
        }

        // Menu-driven system
        static void RunMenu(SqlConnection conn)
        {
            bool exit = false;
            
            while (!exit)
            {
                DisplayMainMenu();
                int choice = GetMenuChoice();
                
                switch (choice)
                {
                    case 1:
                        HandleGuestOperations(conn);
                        break;
                    case 2:
                        HandleRoomTypeOperations(conn);
                        break;
                    case 3:
                        HandleRoomOperations(conn);
                        break;
                    case 4:
                        HandleBookingOperations(conn);
                        break;
                    case 5:
                        HandlePaymentOperations(conn);
                        break;
                    case 6:
                        HandleLoginOperations(conn);
                        break;
                    case 7:
                        DisplayAllTables(conn);
                        break;
                    case 8:
                        exit = true;
                        Console.WriteLine("Thank you for using Hotel Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. Guest Operations");
            Console.WriteLine("2. Room Type Operations");
            Console.WriteLine("3. Room Operations");
            Console.WriteLine("4. Booking Operations");
            Console.WriteLine("5. Payment Operations");
            Console.WriteLine("6. Login User Management");
            Console.WriteLine("7. Display All Tables");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice (1-8): ");
        }

        static int GetMenuChoice()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
        }

        static void HandleGuestOperations(SqlConnection conn)
        {
            Console.WriteLine("\n=== GUEST OPERATIONS ===");
            Console.WriteLine("1. Insert Guest");
            Console.WriteLine("2. Display Guests");
            Console.WriteLine("3. Update Guest");
            Console.WriteLine("4. Delete Guest");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice (1-5): ");
            
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Guest.InsertGuest(conn);
                        break;
                    case 2:
                        Guest.DisplayGuest(conn);
                        break;
                    case 3:
                        Guest.UpdateGuest(conn);
                        break;
                    case 4:
                        Guest.DeleteGuest(conn);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void HandleRoomTypeOperations(SqlConnection conn)
        {
            Console.WriteLine("\n=== ROOM TYPE OPERATIONS ===");
            Console.WriteLine("1. Insert Room Type");
            Console.WriteLine("2. Display Room Types");
            Console.WriteLine("3. Update Room Type");
            Console.WriteLine("4. Delete Room Type");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice (1-5): ");
            
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        RoomType.InsertRoomType(conn);
                        break;
                    case 2:
                        RoomType.DisplayRoomType(conn);
                        break;
                    case 3:
                        RoomType.UpdateRoomType(conn);
                        break;
                    case 4:
                        RoomType.DeleteRoomType(conn);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void HandleRoomOperations(SqlConnection conn)
        {
            Console.WriteLine("\n=== ROOM OPERATIONS ===");
            Console.WriteLine("1. Insert Room");
            Console.WriteLine("2. Display Rooms");
            Console.WriteLine("3. Update Room");
            Console.WriteLine("4. Delete Room");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice (1-5): ");
            
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Room.InsertRoom(conn);
                        break;
                    case 2:
                        Room.DisplayRoom(conn);
                        break;
                    case 3:
                        Room.UpdateRoom(conn);
                        break;
                    case 4:
                        Room.DeleteRoom(conn);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void HandleBookingOperations(SqlConnection conn)
        {
            Console.WriteLine("\n=== BOOKING OPERATIONS ===");
            Console.WriteLine("1. Insert Booking");
            Console.WriteLine("2. Display Bookings");
            Console.WriteLine("3. Update Booking");
            Console.WriteLine("4. Delete Booking");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice (1-5): ");
            
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Booking.InsertBooking(conn);
                        break;
                    case 2:
                        Booking.DisplayBooking(conn);
                        break;
                    case 3:
                        Booking.UpdateBooking(conn);
                        break;
                    case 4:
                        Booking.DeleteBooking(conn);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void HandlePaymentOperations(SqlConnection conn)
        {
            Console.WriteLine("\n=== PAYMENT OPERATIONS ===");
            Console.WriteLine("1. Insert Payment");
            Console.WriteLine("2. Display Payments");
            Console.WriteLine("3. Update Payment");
            Console.WriteLine("4. Delete Payment");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice (1-5): ");
            
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Payment.InsertPayment(conn);
                        break;
                    case 2:
                        Payment.DisplayPayment(conn);
                        break;
                    case 3:
                        Payment.UpdatePayment(conn);
                        break;
                    case 4:
                        Payment.DeletePayment(conn);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void HandleLoginOperations(SqlConnection conn)
        {
            Console.WriteLine("\n=== LOGIN USER MANAGEMENT ===");
            Console.WriteLine("1. Display All Login Users");
            Console.WriteLine("2. Update Login User");
            Console.WriteLine("3. Delete Login User");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice (1-4): ");
            
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Login.DisplayAllUsers(conn);
                        break;
                    case 2:
                        Login.UpdateLogin(conn);
                        break;
                    case 3:
                        Login.DeleteLogin(conn);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void DisplayAllTables(SqlConnection conn)
        {
            Console.WriteLine("\n=== DISPLAYING ALL TABLES ===");
            Guest.DisplayGuest(conn);
            RoomType.DisplayRoomType(conn);
            Room.DisplayRoom(conn);
            Booking.DisplayBooking(conn);
            Payment.DisplayPayment(conn);
            Login.DisplayAllUsers(conn);
        }
    }
    
}
