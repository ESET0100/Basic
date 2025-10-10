using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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

        // Database methods
        public static void InsertRoom(SqlConnection conn)
        {
            Console.WriteLine("\n=== INSERT ROOM ===");

            Console.Write("Enter Room ID: ");
            int roomId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Room Number: ");
            string roomNumber = Console.ReadLine();

            Console.Write("Enter Room Type ID: ");
            int roomTypeId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Floor: ");
            int floor = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Status (Available/Occupied/Maintenance): ");
            string status = Console.ReadLine();

            Console.Write("Enter Price Per Night: ");
            decimal pricePerNight = Convert.ToDecimal(Console.ReadLine());

            try
            {
                string query = "INSERT INTO Rooms (RoomID, RoomNumber, RoomTypeID, Floor, Status, PricePerNight) VALUES (@RoomID, @RoomNumber, @RoomTypeID, @Floor, @Status, @PricePerNight)";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@RoomID", roomId);
                cm.Parameters.AddWithValue("@RoomNumber", roomNumber);
                cm.Parameters.AddWithValue("@RoomTypeID", roomTypeId);
                cm.Parameters.AddWithValue("@Floor", floor);
                cm.Parameters.AddWithValue("@Status", status);
                cm.Parameters.AddWithValue("@PricePerNight", pricePerNight);

                int rows = cm.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Room inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to insert room.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting room: " + ex.Message);
            }
        }

        public static void DisplayRoom(SqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM Rooms";
                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("\n=== ROOM TABLE ===");
                Console.WriteLine("ID | Room Number | Type ID | Floor | Status | Price/Night");
                Console.WriteLine("---|-------------|---------|-------|--------|------------");

                while (reader.Read())
                {
                    int roomId = (int)reader["RoomID"];
                    string roomNumber = reader["RoomNumber"].ToString();
                    int roomTypeId = (int)reader["RoomTypeID"];
                    int floor = (int)reader["Floor"];
                    string status = reader["Status"].ToString();
                    decimal pricePerNight = (decimal)reader["PricePerNight"];

                    Console.WriteLine($"ID: {roomId}, Room: {roomNumber}, TypeID: {roomTypeId}, Floor: {floor}, Status: {status}, Price: ₹{pricePerNight}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying rooms: " + ex.Message);
            }
        }

        public static void UpdateRoom(SqlConnection conn)
        {
            Console.WriteLine("\n=== UPDATE ROOM ===");
            Console.WriteLine("Choose filter field (RoomID/RoomNumber/RoomTypeID/Floor/Status): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "RoomID", "RoomNumber", "RoomTypeID", "Floor", "Status" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            Console.WriteLine("Choose field to update (RoomNumber/RoomTypeID/Floor/Status/PricePerNight): ");
            string updateField = Console.ReadLine();
            string[] upAllowed = { "RoomNumber", "RoomTypeID", "Floor", "Status", "PricePerNight" };
            if (!upAllowed.Contains(updateField))
            {
                Console.WriteLine("Invalid update field.");
                return;
            }
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            try
            {
                string query = $"UPDATE Rooms SET {updateField} = @NewValue WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (updateField == "RoomTypeID" || updateField == "Floor") cm.Parameters.AddWithValue("@NewValue", Convert.ToInt32(newValue));
                else if (updateField == "PricePerNight") cm.Parameters.AddWithValue("@NewValue", Convert.ToDecimal(newValue));
                else cm.Parameters.AddWithValue("@NewValue", newValue);

                if (filterField == "RoomID" || filterField == "RoomTypeID" || filterField == "Floor")
                    cm.Parameters.AddWithValue("@FilterValue", Convert.ToInt32(filterValue));
                else
                    cm.Parameters.AddWithValue("@FilterValue", filterValue);

                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Room updated successfully!" : "No matching records updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating room: " + ex.Message);
            }
        }

        public static void DeleteRoom(SqlConnection conn)
        {
            Console.WriteLine("\n=== DELETE ROOM ===");
            Console.WriteLine("Choose filter field (RoomID/RoomNumber/RoomTypeID/Floor/Status): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "RoomID", "RoomNumber", "RoomTypeID", "Floor", "Status" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            try
            {
                string query = $"DELETE FROM Rooms WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (filterField == "RoomID" || filterField == "RoomTypeID" || filterField == "Floor")
                    cm.Parameters.AddWithValue("@FilterValue", Convert.ToInt32(filterValue));
                else
                    cm.Parameters.AddWithValue("@FilterValue", filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Room(s) deleted successfully!" : "No matching records deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting room: " + ex.Message);
            }
        }
    }
}
