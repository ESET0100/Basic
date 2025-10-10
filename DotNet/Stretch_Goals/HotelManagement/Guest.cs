using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Guest
    {
        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Constructor 1: Most basic - only name and phone
        public Guest(string firstName, string lastName, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }

        // Constructor 2: With email
        public Guest(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        // Constructor 3: All details
        public Guest(string firstName, string lastName, string phone, string email, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public bool IsValidPhone()
        {
            return Phone.Length == 10;
        }

        public string ReturnEmail()
        {
            return Email;
        }

        public string ReturnAddress()
        {
            return Address;
        }
        // Generate guest welcome message
        public string GenerateWelcomeMessage(string roomNumber)
        {
            return $"Welcome {FirstName} {LastName}! Your room {roomNumber} is ready. " +
                   $"We're delighted to have you stay with us. " +
                   $"For any assistance, please contact reception.";
        }

        // Validate all guest information at once
        public string ValidateGuestInformation()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                return "Name is incomplete";

            if (!IsValidPhone())
                return "Phone number must be 10 digits";

            if (!string.IsNullOrEmpty(Email) && !Email.Contains("@"))
                return "Invalid email format";

            return "All guest information is valid";
        }

        // Database methods
        public static void InsertGuest(SqlConnection conn)
        {
            Console.WriteLine("\n=== INSERT GUEST ===");
            
            Console.Write("Enter Guest ID: ");
            int guestId = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            
            Console.Write("Enter Phone (10 digits): ");
            string phone = Console.ReadLine();
            
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            try
            {
                string query = "INSERT INTO Guests (GuestID, FirstName, LastName, Email, Phone, Addr) VALUES (@GuestID, @FirstName, @LastName, @Email, @Phone, @Address)";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@GuestID", guestId);
                cm.Parameters.AddWithValue("@FirstName", firstName);
                cm.Parameters.AddWithValue("@LastName", lastName);
                cm.Parameters.AddWithValue("@Email", email);
                cm.Parameters.AddWithValue("@Phone", phone);
                cm.Parameters.AddWithValue("@Address", address);

                int rows = cm.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Guest inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to insert guest.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting guest: " + ex.Message);
            }
        }

        public static void DisplayGuest(SqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM Guests";
                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("\n=== GUEST TABLE ===");
                Console.WriteLine("ID | First Name | Last Name | Email | Phone | Address");
                Console.WriteLine("---|------------|-----------|-------|-------|---------");
                
                while (reader.Read())
                {
                    int guestId = (int)reader["GuestID"];
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string email = reader["Email"].ToString();
                    string phone = reader["Phone"].ToString();
                    string address = reader["Addr"].ToString();

                    Console.WriteLine($"ID: {guestId}, Name: {firstName} {lastName}, Email: {email}, Phone: {phone}, Address: {address}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying guests: " + ex.Message);
            }
        }

        public static void UpdateGuest(SqlConnection conn)
        {
            Console.WriteLine("\n=== UPDATE GUEST ===");
            Console.WriteLine("Choose filter field (GuestID/FirstName/LastName/Email/Phone): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "GuestID", "FirstName", "LastName", "Email", "Phone" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            Console.WriteLine("Choose field to update (FirstName/LastName/Email/Phone/Addr): ");
            string updateField = Console.ReadLine();
            string[] upAllowed = { "FirstName", "LastName", "Email", "Phone", "Addr" };
            if (!upAllowed.Contains(updateField))
            {
                Console.WriteLine("Invalid update field.");
                return;
            }
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            try
            {
                string query = $"UPDATE Guests SET {updateField} = @NewValue WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@NewValue", newValue);
                cm.Parameters.AddWithValue("@FilterValue", filterField == "GuestID" ? Convert.ToInt32(filterValue) : filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Guest updated successfully!" : "No matching records updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating guest: " + ex.Message);
            }
        }

        public static void DeleteGuest(SqlConnection conn)
        {
            Console.WriteLine("\n=== DELETE GUEST ===");
            Console.WriteLine("Choose filter field (GuestID/FirstName/LastName/Email/Phone): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "GuestID", "FirstName", "LastName", "Email", "Phone" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            try
            {
                string query = $"DELETE FROM Guests WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@FilterValue", filterField == "GuestID" ? Convert.ToInt32(filterValue) : filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Guest(s) deleted successfully!" : "No matching records deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting guest: " + ex.Message);
            }
        }

    }
}
