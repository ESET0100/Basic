using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HotelManagement
{
    internal class Database_conn
    {
        private string connectionString;
        private SqlConnection connection;

        public Database_conn()
        {
            var datasource = @"DESKTOP-49AG27E\SQLEXPRESS"; // your server
            var database = "hotel"; // your database name

            // Create your connection string
            connectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database +
                ";Trusted_Connection=True;" + "TrustServerCertificate=True;";
        }

        public SqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
            }
            return connection;
        }

        public bool OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened successfully!");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening connection: " + ex.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error closing connection: " + ex.Message);
            }
        }

        public void CheckTables()
        {
            try
            {
                Console.WriteLine("\nChecking existing tables in database...");
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                SqlCommand cm = new SqlCommand(query, connection);
                SqlDataReader reader = cm.ExecuteReader();

                Console.WriteLine("Existing tables:");
                while (reader.Read())
                {
                    string tableName = reader["TABLE_NAME"].ToString();
                    Console.WriteLine($"- {tableName}");
                }
                reader.Close();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking tables: " + ex.Message);
            }
        }
    }
}