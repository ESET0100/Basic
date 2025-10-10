using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class RoomType
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public int Capacity { get; set; }
        public decimal BasePrice { get; set; }

        // Constructor 1: Most basic - name and price
        public RoomType(string typeName, decimal basePrice)
        {
            TypeName = typeName;
            BasePrice = basePrice;
        }

        // Constructor 2: With capacity
        public RoomType(string typeName, int capacity, decimal basePrice)
        {
            TypeName = typeName;
            Capacity = capacity;
            BasePrice = basePrice;
        }

        public bool CanAccommodate(int guests)
        {
            return guests <= Capacity;
        }

        public decimal CalculatePrice(int nights)
        {
            return BasePrice * nights;
        }

        public string GetTypeById(int id)
        {
            if (this.RoomTypeID == id)
            {
                return this.TypeName;
            }
            return "Room type not found";
        }
        // Calculate seasonal pricing
        public decimal CalculateSeasonalPrice(bool isPeakSeason, bool isWeekend)
        {
            decimal price = BasePrice;

            if (isPeakSeason)
                price *= 1.20m; // 20% increase in peak season

            if (isWeekend)
                price *= 1.15m; // 15% increase on weekends

            return Math.Round(price, 2);
        }

        // Suggest room based on guest count and budget
        public string SuggestRoom(int guestCount, decimal maxBudget)
        {
            if (Capacity >= guestCount && BasePrice <= maxBudget)
                return $"Perfect match: {TypeName} can accommodate {guestCount} guests at ₹{BasePrice}/night";
            else if (Capacity >= guestCount)
                return $"{TypeName} fits {guestCount} guests but exceeds budget by ₹{BasePrice - maxBudget}";
            else
                return $"{TypeName} cannot accommodate {guestCount} guests (max: {Capacity})";
        }

        // Database methods
        public static void InsertRoomType(SqlConnection conn)
        {
            Console.WriteLine("\n=== INSERT ROOM TYPE ===");
            
            Console.Write("Enter Room Type ID: ");
            int roomTypeId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Type Name: ");
            string typeName = Console.ReadLine();
            
            Console.Write("Enter Capacity: ");
            int capacity = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter Base Price: ");
            decimal basePrice = Convert.ToDecimal(Console.ReadLine());

            try
            {
                string query = "INSERT INTO RoomTypes (RoomTypeID, TypeName, Capacity, BasePrice) VALUES (@RoomTypeID, @TypeName, @Capacity, @BasePrice)";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@RoomTypeID", roomTypeId);
                cm.Parameters.AddWithValue("@TypeName", typeName);
                cm.Parameters.AddWithValue("@Capacity", capacity);
                cm.Parameters.AddWithValue("@BasePrice", basePrice);

                int rows = cm.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Room Type inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to insert room type.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting room type: " + ex.Message);
            }
        }

        public static void DisplayRoomType(SqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM RoomTypes";
                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("\n=== ROOM TYPE TABLE ===");
                Console.WriteLine("ID | Type Name | Capacity | Base Price");
                Console.WriteLine("---|-----------|----------|-----------");
                
                while (reader.Read())
                {
                    int roomTypeId = (int)reader["RoomTypeID"];
                    string typeName = reader["TypeName"].ToString();
                    int capacity = (int)reader["Capacity"];
                    decimal basePrice = (decimal)reader["BasePrice"];

                    Console.WriteLine($"ID: {roomTypeId}, Type: {typeName}, Capacity: {capacity}, Price: ₹{basePrice}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying room types: " + ex.Message);
            }
        }

        public static void UpdateRoomType(SqlConnection conn)
        {
            Console.WriteLine("\n=== UPDATE ROOM TYPE ===");
            Console.WriteLine("Choose filter field (RoomTypeID/TypeName): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "RoomTypeID", "TypeName" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            Console.WriteLine("Choose field to update (TypeName/Capacity/BasePrice): ");
            string updateField = Console.ReadLine();
            string[] upAllowed = { "TypeName", "Capacity", "BasePrice" };
            if (!upAllowed.Contains(updateField))
            {
                Console.WriteLine("Invalid update field.");
                return;
            }
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            try
            {
                string query = $"UPDATE RoomTypes SET {updateField} = @NewValue WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                if (updateField == "Capacity") cm.Parameters.AddWithValue("@NewValue", Convert.ToInt32(newValue));
                else if (updateField == "BasePrice") cm.Parameters.AddWithValue("@NewValue", Convert.ToDecimal(newValue));
                else cm.Parameters.AddWithValue("@NewValue", newValue);
                cm.Parameters.AddWithValue("@FilterValue", filterField == "RoomTypeID" ? Convert.ToInt32(filterValue) : filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Room type updated successfully!" : "No matching records updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating room type: " + ex.Message);
            }
        }

        public static void DeleteRoomType(SqlConnection conn)
        {
            Console.WriteLine("\n=== DELETE ROOM TYPE ===");
            Console.WriteLine("Choose filter field (RoomTypeID/TypeName): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "RoomTypeID", "TypeName" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            try
            {
                string query = $"DELETE FROM RoomTypes WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@FilterValue", filterField == "RoomTypeID" ? Convert.ToInt32(filterValue) : filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Room type(s) deleted successfully!" : "No matching records deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting room type: " + ex.Message);
            }
        }

    }
}
