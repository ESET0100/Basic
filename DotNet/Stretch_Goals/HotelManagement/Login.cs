using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Login
    {
        public string Username { get; set; }
        public string StaffName { get; set; }
        public string Password { get; set; }

        // Constructor for new users
        public Login(string username, string staffName, string password)
        {
            Username = username;
            StaffName = staffName;
            Password = password;
        }

        // Database methods
        public static bool AuthenticateUser(SqlConnection conn, string username, string password)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Login WHERE username = @Username AND passwd = @Password";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@Username", username);
                cm.Parameters.AddWithValue("@Password", password);

                int count = (int)cm.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error authenticating user: " + ex.Message);
                return false;
            }
        }

        public static bool RegisterNewUser(SqlConnection conn, string username, string staffName, string password)
        {
            try
            {
                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Login WHERE username = @Username";
                SqlCommand checkCm = new SqlCommand(checkQuery, conn);
                checkCm.Parameters.AddWithValue("@Username", username);

                int existingCount = (int)checkCm.ExecuteScalar();
                if (existingCount > 0)
                {
                    Console.WriteLine("Username already exists! Please choose a different username.");
                    return false;
                }

                // Insert new user
                string insertQuery = "INSERT INTO Login (username, staff_name, passwd) VALUES (@Username, @StaffName, @Password)";
                SqlCommand insertCm = new SqlCommand(insertQuery, conn);
                insertCm.Parameters.AddWithValue("@Username", username);
                insertCm.Parameters.AddWithValue("@StaffName", staffName);
                insertCm.Parameters.AddWithValue("@Password", password);

                int rows = insertCm.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering new user: " + ex.Message);
                return false;
            }
        }

        public static void DisplayAllUsers(SqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM Login ORDER BY username";
                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("\n=== LOGIN USERS TABLE ===");
                Console.WriteLine("Username | Staff Name | Password");
                Console.WriteLine("---------|------------|----------");

                while (reader.Read())
                {
                    string username = reader["username"].ToString();
                    string staffName = reader["staff_name"].ToString();
                    string password = reader["passwd"].ToString();

                    Console.WriteLine($"Username: {username}, Staff Name: {staffName}, Password: {password}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying users: " + ex.Message);
            }
        }

        public static void UpdateLogin(SqlConnection conn)
        {
            Console.WriteLine("\n=== UPDATE LOGIN USER ===");
            Console.WriteLine("Choose filter field (username/staff_name): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "username", "staff_name" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            Console.WriteLine("Choose field to update (staff_name/passwd): ");
            string updateField = Console.ReadLine();
            string[] upAllowed = { "staff_name", "passwd" };
            if (!upAllowed.Contains(updateField))
            {
                Console.WriteLine("Invalid update field.");
                return;
            }
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            try
            {
                string query = $"UPDATE Login SET {updateField} = @NewValue WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@NewValue", newValue);
                cm.Parameters.AddWithValue("@FilterValue", filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Login user updated successfully!" : "No matching records updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating login user: " + ex.Message);
            }
        }

        public static void DeleteLogin(SqlConnection conn)
        {
            Console.WriteLine("\n=== DELETE LOGIN USER ===");
            Console.WriteLine("Choose filter field (username/staff_name): ");
            string filterField = Console.ReadLine();
            string[] allowed = { "username", "staff_name" };
            if (!allowed.Contains(filterField))
            {
                Console.WriteLine("Invalid field.");
                return;
            }
            Console.Write("Enter filter value: ");
            string filterValue = Console.ReadLine();

            try
            {
                string query = $"DELETE FROM Login WHERE {filterField} = @FilterValue";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@FilterValue", filterValue);
                int rows = cm.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Login user(s) deleted successfully!" : "No matching records deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting login user: " + ex.Message);
            }
        }

        public static bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username and password cannot be empty!");
                return false;
            }

            if (username.Length < 3)
            {
                Console.WriteLine("Username must be at least 3 characters long!");
                return false;
            }

            if (password.Length < 4)
            {
                Console.WriteLine("Password must be at least 4 characters long!");
                return false;
            }

            return true;
        }
    }
}
