using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
