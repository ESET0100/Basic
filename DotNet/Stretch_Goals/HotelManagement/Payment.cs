using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
