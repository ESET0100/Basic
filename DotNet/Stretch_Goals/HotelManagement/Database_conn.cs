using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Database_conn
    {
        //        static void Main(string[] args)
        //        {
        //            Console.WriteLine("Hotel Management System");
        //            Console.WriteLine("Getting Connection ...");

        //            var datasource = @"DESKTOP-49AG27E\SQLEXPRESS"; // your server
        //            var database = "hotel"; // your database name

        //            // Create your connection string
        //            string connString = @"Data Source=" + datasource +
        //                ";Initial Catalog=" + database + "; Trusted_Connection=True;";

        //            Console.WriteLine(connString);

        //            SqlConnection conn = new SqlConnection(connString);

        //            try
        //            {
        //                Console.WriteLine("Opening Connection ...");
        //                // Open the connection
        //                conn.Open();
        //                Console.WriteLine("Connection successful!");

        //                // Insert sample data into all tables
        //                InsertGuest(conn);
        //                InsertRoomType(conn);
        //                InsertRoom(conn);
        //                InsertBooking(conn);
        //                InsertPayment(conn);

        //                // Display all tables
        //                DisplayGuest(conn);
        //                DisplayRoomType(conn);
        //                DisplayRoom(conn);
        //                DisplayBooking(conn);
        //                DisplayPayment(conn);
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine("Error: " + e.Message);
        //            }
        //            finally
        //            {
        //                // Close the connection
        //                conn.Close();
        //            }
        //        }

        //        // Guest table methods
        //        static void InsertGuest(SqlConnection conn)
        //        {
        //            Console.WriteLine("Inserting Guest...");
        //            string query = "INSERT INTO Guest (GuestID, FirstName, LastName, Email, Phone, Addr) VALUES (@GuestID, @FirstName, @LastName, @Email, @Phone, @Address)";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            cm.Parameters.AddWithValue("@GuestID", 10);
        //            cm.Parameters.AddWithValue("@FirstName", "Lakshay");
        //            cm.Parameters.AddWithValue("@LastName", "Saxena");
        //            cm.Parameters.AddWithValue("@Email", "Lakshaysaxena13@email.com");
        //            cm.Parameters.AddWithValue("@Phone", "7905687703");
        //            cm.Parameters.AddWithValue("@Address", "Joy Apartment, New Delhi");

        //            int rows = cm.ExecuteNonQuery();
        //            if (rows > 0)
        //            {
        //                Console.WriteLine("Guest inserted successfully");
        //            }
        //        }

        //        static void DisplayGuest(SqlConnection conn)
        //        {
        //            string query = "SELECT * FROM Guest";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            SqlDataReader reader = cm.ExecuteReader();

        //            Console.WriteLine("\nGuest Table:");
        //            while (reader.Read())
        //            {
        //                int guestId = (int)reader["GuestID"];
        //                string firstName = reader["FirstName"].ToString();
        //                string lastName = reader["LastName"].ToString();
        //                string email = reader["Email"].ToString();
        //                string phone = reader["Phone"].ToString();
        //                string address = reader["Address"].ToString();

        //                Console.WriteLine($"ID: {guestId}, Name: {firstName} {lastName}, Email: {email}, Phone: {phone}, Address: {address}");
        //            }
        //            reader.Close();
        //        }

        //        // RoomType table methods
        //        static void InsertRoomType(SqlConnection conn)
        //        {
        //            Console.WriteLine("Inserting Room Type...");
        //            string query = "INSERT INTO RoomType (RoomTypeID, TypeName, Capacity, BasePrice) VALUES (@RoomTypeID, @TypeName, @Capacity, @BasePrice)";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            cm.Parameters.AddWithValue("@RoomTypeID", 10);
        //            cm.Parameters.AddWithValue("@TypeName", "House");
        //            cm.Parameters.AddWithValue("@Capacity", 10);
        //            cm.Parameters.AddWithValue("@BasePrice", 25000.00);

        //            int rows = cm.ExecuteNonQuery();
        //            if (rows > 0)
        //            {
        //                Console.WriteLine("Room Type inserted successfully");
        //            }
        //        }

        //        static void DisplayRoomType(SqlConnection conn)
        //        {
        //            string query = "SELECT * FROM RoomType";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            SqlDataReader reader = cm.ExecuteReader();

        //            Console.WriteLine("\nRoomType Table:");
        //            while (reader.Read())
        //            {
        //                int roomTypeId = (int)reader["RoomTypeID"];
        //                string typeName = reader["TypeName"].ToString();
        //                int capacity = (int)reader["Capacity"];
        //                decimal basePrice = (decimal)reader["BasePrice"];

        //                Console.WriteLine($"ID: {roomTypeId}, Type: {typeName}, Capacity: {capacity}, Price: ₹{basePrice}");
        //            }
        //            reader.Close();
        //        }

        //        // Room table methods
        //        static void InsertRoom(SqlConnection conn)
        //        {
        //            Console.WriteLine("Inserting Room...");
        //            string query = "INSERT INTO Room (RoomID, RoomNumber, RoomTypeID, Floor, Status, PricePerNight) VALUES (@RoomID, @RoomNumber, @RoomTypeID, @Floor, @Status, @PricePerNight)";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            cm.Parameters.AddWithValue("@RoomID", 10);
        //            cm.Parameters.AddWithValue("@RoomNumber", "701");
        //            cm.Parameters.AddWithValue("@RoomTypeID", 10);
        //            cm.Parameters.AddWithValue("@Floor", 2);
        //            cm.Parameters.AddWithValue("@Status", "Available");
        //            cm.Parameters.AddWithValue("@PricePerNight", 2500.00);

        //            int rows = cm.ExecuteNonQuery();
        //            if (rows > 0)
        //            {
        //                Console.WriteLine("Room inserted successfully");
        //            }
        //        }

        //        static void DisplayRoom(SqlConnection conn)
        //        {
        //            string query = "SELECT * FROM Room";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            SqlDataReader reader = cm.ExecuteReader();

        //            Console.WriteLine("\nRoom Table:");
        //            while (reader.Read())
        //            {
        //                int roomId = (int)reader["RoomID"];
        //                string roomNumber = reader["RoomNumber"].ToString();
        //                int roomTypeId = (int)reader["RoomTypeID"];
        //                int floor = (int)reader["Floor"];
        //                string status = reader["Status"].ToString();
        //                decimal pricePerNight = (decimal)reader["PricePerNight"];

        //                Console.WriteLine($"ID: {roomId}, Room: {roomNumber}, TypeID: {roomTypeId}, Floor: {floor}, Status: {status}, Price: ₹{pricePerNight}");
        //            }
        //            reader.Close();
        //        }

        //        // Booking table methods
        //        static void InsertBooking(SqlConnection conn)
        //        {
        //            Console.WriteLine("Inserting Booking...");
        //            string query = "INSERT INTO Booking (BookingID, GuestID, RoomID, CheckInDate, CheckOutDate, TotalAmount, Status) VALUES (@BookingID, @GuestID, @RoomID, @CheckInDate, @CheckOutDate, @TotalAmount, @Status)";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            cm.Parameters.AddWithValue("@BookingID", 10);
        //            cm.Parameters.AddWithValue("@GuestID", 10);
        //            cm.Parameters.AddWithValue("@RoomID", 10);
        //            cm.Parameters.AddWithValue("@CheckInDate", DateTime.Now);
        //            cm.Parameters.AddWithValue("@CheckOutDate", DateTime.Now.AddDays(2));
        //            cm.Parameters.AddWithValue("@TotalAmount", 5000.00);
        //            cm.Parameters.AddWithValue("@Status", "Confirmed");

        //            int rows = cm.ExecuteNonQuery();
        //            if (rows > 0)
        //            {
        //                Console.WriteLine("Booking inserted successfully");
        //            }
        //        }

        //        static void DisplayBooking(SqlConnection conn)
        //        {
        //            string query = "SELECT * FROM Booking";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            SqlDataReader reader = cm.ExecuteReader();

        //            Console.WriteLine("\nBooking Table:");
        //            while (reader.Read())
        //            {
        //                int bookingId = (int)reader["BookingID"];
        //                int guestId = (int)reader["GuestID"];
        //                int roomId = (int)reader["RoomID"];
        //                DateTime checkInDate = (DateTime)reader["CheckInDate"];
        //                DateTime checkOutDate = (DateTime)reader["CheckOutDate"];
        //                decimal totalAmount = (decimal)reader["TotalAmount"];
        //                string status = reader["Status"].ToString();

        //                Console.WriteLine($"ID: {bookingId}, GuestID: {guestId}, RoomID: {roomId}, CheckIn: {checkInDate:dd/MM/yyyy}, CheckOut: {checkOutDate:dd/MM/yyyy}, Amount: ₹{totalAmount}, Status: {status}");
        //            }
        //            reader.Close();
        //        }

        //        // Payment table methods
        //        static void InsertPayment(SqlConnection conn)
        //        {
        //            Console.WriteLine("Inserting Payment...");
        //            string query = "INSERT INTO Payment (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate, Status) VALUES (@PaymentID, @BookingID, @Amount, @PaymentMethod, @PaymentDate, @Status)";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            cm.Parameters.AddWithValue("@PaymentID", 10);
        //            cm.Parameters.AddWithValue("@BookingID", 10);
        //            cm.Parameters.AddWithValue("@Amount", 5000.00);
        //            cm.Parameters.AddWithValue("@PaymentMethod", "Credit Card");
        //            cm.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
        //            cm.Parameters.AddWithValue("@Status", "Completed");

        //            int rows = cm.ExecuteNonQuery();
        //            if (rows > 0)
        //            {
        //                Console.WriteLine("Payment inserted successfully");
        //            }
        //        }

        //        static void DisplayPayment(SqlConnection conn)
        //        {
        //            string query = "SELECT * FROM Payment";
        //            SqlCommand cm = new SqlCommand(query, conn);
        //            SqlDataReader reader = cm.ExecuteReader();

        //            Console.WriteLine("\nPayment Table:");
        //            while (reader.Read())
        //            {
        //                int paymentId = (int)reader["PaymentID"];
        //                int bookingId = (int)reader["BookingID"];
        //                decimal amount = (decimal)reader["Amount"];
        //                string paymentMethod = reader["PaymentMethod"].ToString();
        //                DateTime paymentDate = (DateTime)reader["PaymentDate"];
        //                string status = reader["Status"].ToString();

        //                Console.WriteLine($"ID: {paymentId}, BookingID: {bookingId}, Amount: ₹{amount}, Method: {paymentMethod}, Date: {paymentDate:dd/MM/yyyy}, Status: {status}");
        //            }
        //            reader.Close();
        //        }
        //    }
        //}
    }
}