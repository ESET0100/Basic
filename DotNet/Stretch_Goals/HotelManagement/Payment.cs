using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Payment
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }

        // Constructor 1: Most basic - booking and amount
        public Payment(int bookingID, decimal amount)
        {
            BookingID = bookingID;
            Amount = amount;
            PaymentDate = DateTime.Now; // Default to current date
            Status = "Pending"; // Default status
        }

        // Constructor 2: With payment method
        public Payment(int bookingID, decimal amount, string paymentMethod)
        {
            BookingID = bookingID;
            Amount = amount;
            PaymentMethod = paymentMethod;
            PaymentDate = DateTime.Now;
            Status = "Pending";
        }

        // Constructor 3: Complete details
        public Payment(int bookingID, decimal amount, string paymentMethod, DateTime paymentDate, string status)
        {
            BookingID = bookingID;
            Amount = amount;
            PaymentMethod = paymentMethod;
            PaymentDate = paymentDate;
            Status = status;
        }

        public bool IsPaymentSuccessful()
        {
            return Status == "Completed";
        }

        public bool IsCashPayment()
        {
            return PaymentMethod == "Cash";
        }

        public void MarkAsCompleted()
        {
            Status = "Completed";
        }
        // Calculate payment processing fee
        public decimal CalculateProcessingFee()
        {
            decimal fee = 0;
            if (PaymentMethod == "Credit Card" || PaymentMethod == "Debit Card")
                fee = Amount * 0.02m; // 2% processing fee
            else if (PaymentMethod == "Online Transfer")
                fee = Amount * 0.01m; // 1% processing fee

            return Math.Round(fee, 2);
        }

        // Generate payment receipt
        public string GenerateReceipt(string guestName, string bookingReference)
        {
            return $"PAYMENT RECEIPT\n" +
                   $"---------------\n" +
                   $"Guest: {guestName}\n" +
                   $"Booking Ref: {bookingReference}\n" +
                   $"Amount: ₹{Amount}\n" +
                   $"Method: {PaymentMethod}\n" +
                   $"Date: {PaymentDate:dd MMM yyyy HH:mm}\n" +
                   $"Status: {Status}\n" +
                   $"Processing Fee: ₹{CalculateProcessingFee()}\n" +
                   $"Total Paid: ₹{Amount + CalculateProcessingFee()}";
        }

        // Database methods
        public static void InsertPayment(SqlConnection conn)
        {
            Console.WriteLine("\n=== INSERT PAYMENT ===");
            
            Console.Write("Enter Payment ID: ");
            int paymentId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Booking ID: ");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            
            Console.Write("Enter Payment Method (Cash/Credit Card/Debit Card/Online Transfer): ");
            string paymentMethod = Console.ReadLine();
            
            Console.Write("Enter Payment Date (yyyy-mm-dd hh:mm:ss) or press Enter for current time: ");
            string dateInput = Console.ReadLine();
            DateTime paymentDate = string.IsNullOrEmpty(dateInput) ? DateTime.Now : Convert.ToDateTime(dateInput);
            
            Console.Write("Enter Status (Pending/Completed/Failed): ");
            string status = Console.ReadLine();

            try
            {
                string query = "INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate, Status) VALUES (@PaymentID, @BookingID, @Amount, @PaymentMethod, @PaymentDate, @Status)";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@PaymentID", paymentId);
                cm.Parameters.AddWithValue("@BookingID", bookingId);
                cm.Parameters.AddWithValue("@Amount", amount);
                cm.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                cm.Parameters.AddWithValue("@PaymentDate", paymentDate);
                cm.Parameters.AddWithValue("@Status", status);

                int rows = cm.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Payment inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to insert payment.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting payment: " + ex.Message);
            }
        }

        public static void DisplayPayment(SqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM Payments";
                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("\n=== PAYMENT TABLE ===");
                Console.WriteLine("ID | Booking ID | Amount | Method | Date | Status");
                Console.WriteLine("---|------------|--------|--------|------|--------");
                
                while (reader.Read())
                {
                    int paymentId = (int)reader["PaymentID"];
                    int bookingId = (int)reader["BookingID"];
                    decimal amount = (decimal)reader["Amount"];
                    string paymentMethod = reader["PaymentMethod"].ToString();
                    DateTime paymentDate = (DateTime)reader["PaymentDate"];
                    string status = reader["Status"].ToString();

                    Console.WriteLine($"ID: {paymentId}, BookingID: {bookingId}, Amount: ₹{amount}, Method: {paymentMethod}, Date: {paymentDate:dd/MM/yyyy}, Status: {status}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying payments: " + ex.Message);
            }
        }

        public static void UpdatePayment(SqlConnection conn)
        {
            Console.WriteLine("\n=== UPDATE PAYMENT ===");
            Console.WriteLine("Choose filter field (PaymentID/BookingID/Status/PaymentMethod): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "PaymentID", "BookingID", "Status", "PaymentMethod" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            Console.WriteLine("Choose field to update (BookingID/Amount/PaymentMethod/PaymentDate/Status): ");
            string updateField = Console.ReadLine();
            string[] upAllowed = { "BookingID", "Amount", "PaymentMethod", "PaymentDate", "Status" };
            if (!upAllowed.Contains(updateField))
            {
                Console.WriteLine("Invalid update field.");
                return;
            }
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            try
            {
                string query = $"UPDATE Payments SET {updateField} = @NewValue WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (updateField == "BookingID") cm.Parameters.AddWithValue("@NewValue", Convert.ToInt32(newValue));
                else if (updateField == "Amount") cm.Parameters.AddWithValue("@NewValue", Convert.ToDecimal(newValue));
                else if (updateField == "PaymentDate") cm.Parameters.AddWithValue("@NewValue", Convert.ToDateTime(newValue));
                else cm.Parameters.AddWithValue("@NewValue", newValue);

                if (filterField == "PaymentID" || filterField == "BookingID")
                    cm.Parameters.AddWithValue("@FilterValue", Convert.ToInt32(filterValue));
                else
                    cm.Parameters.AddWithValue("@FilterValue", filterValue);

                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Payment updated successfully!" : "No matching records updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating payment: " + ex.Message);
            }
        }

        public static void DeletePayment(SqlConnection conn)
        {
            Console.WriteLine("\n=== DELETE PAYMENT ===");
            Console.WriteLine("Choose filter field (PaymentID/BookingID/Status/PaymentMethod): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "PaymentID", "BookingID", "Status", "PaymentMethod" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            try
            {
                string query = $"DELETE FROM Payments WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (filterField == "PaymentID" || filterField == "BookingID")
                    cm.Parameters.AddWithValue("@FilterValue", Convert.ToInt32(filterValue));
                else
                    cm.Parameters.AddWithValue("@FilterValue", filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Payment(s) deleted successfully!" : "No matching records deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting payment: " + ex.Message);
            }
        }
    }
}
