using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Booking
    {
        public int BookingID { get; set; }
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        // Constructor 1: Most basic - guest, room and dates
        public Booking(int guestID, int roomID, DateTime checkInDate, DateTime checkOutDate)
        {
            GuestID = guestID;
            RoomID = roomID;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = "Confirmed"; // Default status
        }

        // Constructor 2: With total amount
        public Booking(int guestID, int roomID, DateTime checkInDate, DateTime checkOutDate, decimal totalAmount)
        {
            GuestID = guestID;
            RoomID = roomID;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalAmount = totalAmount;
            Status = "Confirmed";
        }

        // Constructor 3: With status
        public Booking(int guestID, int roomID, DateTime checkInDate, DateTime checkOutDate, decimal totalAmount, string status)
        {
            GuestID = guestID;
            RoomID = roomID;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalAmount = totalAmount;
            Status = status;
        }

        public int CalculateNights()
        {
            return (CheckOutDate - CheckInDate).Days;
        }

        public bool IsActive()
        {
            return Status == "Confirmed" || Status == "Checked-In";
        }

        public void CancelBooking()
        {
            Status = "Cancelled";
        }
        // Calculate early check-in/late check-out charges
        public decimal CalculateExtendedStayCharges(bool earlyCheckin, bool lateCheckout)
        {
            decimal charges = 0;
            if (earlyCheckin) charges += 500; // ₹500 for early check-in
            if (lateCheckout) charges += 800; // ₹800 for late check-out
            return charges;
        }

        // Generate booking confirmation message
        public string GenerateConfirmationMessage(string guestName, string roomNumber)
        {
            return $"Dear {guestName}, your booking is confirmed! " +
                   $"Room: {roomNumber}, Check-in: {CheckInDate:dd MMM yyyy}, " +
                   $"Check-out: {CheckOutDate:dd MMM yyyy}, Total: ₹{TotalAmount}. " +
                   $"Booking ID: {BookingID}";
        }

        // Database methods
        public static void InsertBooking(SqlConnection conn)
        {
            Console.WriteLine("\n=== INSERT BOOKING ===");
            
            Console.Write("Enter Booking ID: ");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Guest ID: ");
            int guestId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Room ID: ");
            int roomId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Check-in Date (yyyy-mm-dd): ");
            DateTime checkInDate = Convert.ToDateTime(Console.ReadLine());
            
            Console.Write("Enter Check-out Date (yyyy-mm-dd): ");
            DateTime checkOutDate = Convert.ToDateTime(Console.ReadLine());
            
            Console.Write("Enter Total Amount: ");
            decimal totalAmount = Convert.ToDecimal(Console.ReadLine());
            
            Console.Write("Enter Status (Confirmed/Checked-In/Cancelled): ");
            string status = Console.ReadLine();

            try
            {
                string query = "INSERT INTO Bookings (BookingID, GuestID, RoomID, CheckInDate, CheckOutDate, TotalAmount, Status) VALUES (@BookingID, @GuestID, @RoomID, @CheckInDate, @CheckOutDate, @TotalAmount, @Status)";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@BookingID", bookingId);
                cm.Parameters.AddWithValue("@GuestID", guestId);
                cm.Parameters.AddWithValue("@RoomID", roomId);
                cm.Parameters.AddWithValue("@CheckInDate", checkInDate);
                cm.Parameters.AddWithValue("@CheckOutDate", checkOutDate);
                cm.Parameters.AddWithValue("@TotalAmount", totalAmount);
                cm.Parameters.AddWithValue("@Status", status);

                int rows = cm.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Booking inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to insert booking.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting booking: " + ex.Message);
            }
        }

        public static void DisplayBooking(SqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM Bookings";
                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("\n=== BOOKING TABLE ===");
                Console.WriteLine("ID | Guest ID | Room ID | Check-In | Check-Out | Amount | Status");
                Console.WriteLine("---|----------|---------|----------|-----------|--------|--------");
                
                while (reader.Read())
                {
                    int bookingId = (int)reader["BookingID"];
                    int guestId = (int)reader["GuestID"];
                    int roomId = (int)reader["RoomID"];
                    DateTime checkInDate = (DateTime)reader["CheckInDate"];
                    DateTime checkOutDate = (DateTime)reader["CheckOutDate"];
                    decimal totalAmount = (decimal)reader["TotalAmount"];
                    string status = reader["Status"].ToString();


                    Console.WriteLine($"ID: {bookingId}, GuestID: {guestId}, RoomID: {roomId}, CheckIn: {checkInDate:dd/MM/yyyy}, CheckOut: {checkOutDate:dd/MM/yyyy}, Amount: ₹{totalAmount}, Status: {status}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying bookings: " + ex.Message);
            }
        }

        public static void UpdateBooking(SqlConnection conn)
        {
            Console.WriteLine("\n=== UPDATE BOOKING ===");
            Console.WriteLine("Choose filter field (BookingID/GuestID/RoomID/Status): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "BookingID", "GuestID", "RoomID", "Status" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            Console.WriteLine("Choose field to update (GuestID/RoomID/CheckInDate/CheckOutDate/TotalAmount/Status): ");
            string updateField = Console.ReadLine();
            string[] upAllowed = { "GuestID", "RoomID", "CheckInDate", "CheckOutDate", "TotalAmount", "Status" };
            if (!upAllowed.Contains(updateField))
            {
                Console.WriteLine("Invalid update field.");
                return;
            }
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            try
            {
                string query = $"UPDATE Bookings SET {updateField} = @NewValue WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (updateField == "GuestID" || updateField == "RoomID") cm.Parameters.AddWithValue("@NewValue", Convert.ToInt32(newValue));
                else if (updateField == "TotalAmount") cm.Parameters.AddWithValue("@NewValue", Convert.ToDecimal(newValue));
                else if (updateField == "CheckInDate" || updateField == "CheckOutDate") cm.Parameters.AddWithValue("@NewValue", Convert.ToDateTime(newValue));
                else cm.Parameters.AddWithValue("@NewValue", newValue);

                if (filterField == "BookingID" || filterField == "GuestID" || filterField == "RoomID")
                    cm.Parameters.AddWithValue("@FilterValue", Convert.ToInt32(filterValue));
                else
                    cm.Parameters.AddWithValue("@FilterValue", filterValue);

                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Booking updated successfully!" : "No matching records updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating booking: " + ex.Message);
            }
        }

        public static void DeleteBooking(SqlConnection conn)
        {
            Console.WriteLine("\n=== DELETE BOOKING ===");
            Console.WriteLine("Choose filter field (BookingID/GuestID/RoomID/Status): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "BookingID", "GuestID", "RoomID", "Status" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            try
            {
                string query = $"DELETE FROM Bookings WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (filterField == "BookingID" || filterField == "GuestID" || filterField == "RoomID")
                    cm.Parameters.AddWithValue("@FilterValue", Convert.ToInt32(filterValue));
                else
                    cm.Parameters.AddWithValue("@FilterValue", filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Booking(s) deleted successfully!" : "No matching records deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting booking: " + ex.Message);
            }
        }
    }
}
