using System.Data.SqlClient;
namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Getting Connection ...");

            var datasource = @"DESKTOP-49AG27E\SQLEXPRESS"; // your server
            var database = "dotnet_1"; // your database name

            // Create your connection string
            string connString = @"Data Source=" + datasource +
                ";Initial Catalog=" + database + "; Trusted_Connection=True;";

            Console.WriteLine(connString);

            SqlConnection conn = new SqlConnection(connString);

            try
            {
                Console.WriteLine("Opening Connection ...");
                // Open the connection
                conn.Open();
                Console.WriteLine("Connection successful!");
                InsertStaff(conn);
                displayStaff(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }

        static void InsertStaff(SqlConnection conn)
        {
            Console.Write("Enter the food id and food name...");

            int food_id = 3;
            string food_name = "Pasta";

            // Use parameterized query to prevent SQL injection
            string query = "INSERT INTO food (food_id, name) VALUES (@food_id, @name)";
            SqlCommand cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@food_id", food_id);
            cm.Parameters.AddWithValue("@name", food_name);

            int rows = cm.ExecuteNonQuery();
            if (rows > 0)
            {
                Console.WriteLine("Inserted records successfully");
            }
        }

        static void displayStaff(SqlConnection conn)
        {
            string query = "SELECT * FROM food";
            SqlCommand cm = new SqlCommand(query, conn);
            SqlDataReader reader = cm.ExecuteReader();

            Console.WriteLine("Food Items:");
            while (reader.Read())
            {
                // Use the actual column names from your database
                int foodId = (int)reader["food_id"];
                string foodName = reader["name"].ToString();

                Console.WriteLine($"ID: {foodId}, Name: {foodName}");
            }
            reader.Close(); // Important: Close the reader when done
        }
    }
}