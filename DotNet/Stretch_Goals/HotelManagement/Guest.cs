using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
