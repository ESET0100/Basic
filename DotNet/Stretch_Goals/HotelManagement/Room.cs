using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    internal class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeID { get; set; }
        public int Floor { get; set; }
        public string Status { get; set; }
        public decimal PricePerNight { get; set; }

        // Constructor 1: Most basic - room number and type
        public Room(string roomNumber, int roomTypeID)
        {
            RoomNumber = roomNumber;
            RoomTypeID = roomTypeID;
            Status = "Available"; // Default status
        }

        // Constructor 2: With price
        public Room(string roomNumber, int roomTypeID, decimal pricePerNight)
        {
            RoomNumber = roomNumber;
            RoomTypeID = roomTypeID;
            PricePerNight = pricePerNight;
            Status = "Available";
        }

        // Constructor 3: With floor and status
        public Room(string roomNumber, int roomTypeID, decimal pricePerNight, int floor, string status)
        {
            RoomNumber = roomNumber;
            RoomTypeID = roomTypeID;
            PricePerNight = pricePerNight;
            Floor = floor;
            Status = status;
        }

        public bool IsAvailable()
        {
            return Status == "Available";
        }

        public bool IsNotAvailable()
        {
            return Status == "Occupied";
        }

        public void MarkAsOccupied()
        {
            Status = "Occupied";
        }

        public void MarkAsAvailable()
        {
            Status = "Available";
        }
        // Calculate cleaning time needed
        public int CalculateCleaningTime()
        {
            if (Status == "Checked-Out")
                return 45; // 45 minutes for check-out cleaning
            else if (Status == "Occupied")
                return 20; // 20 minutes for daily cleaning
            else
                return 15; // 15 minutes for maintenance cleaning
        }

        // Check if room is suitable for specific needs
        public string CheckSuitability(int guestCount, bool requiresElevator, bool withChildren)
        {
            string suitability = $"Room {RoomNumber} is ";

            if (Floor > 2 && !requiresElevator && withChildren)
                suitability += "not ideal for children (higher floor)";
            else if (Floor == 1 && withChildren)
                suitability += "perfect for families with children";
            else if (Floor <= 2)
                suitability += "easily accessible without elevator";
            else
                suitability += "suitable for your stay";

            return suitability;

        }
    }
}
