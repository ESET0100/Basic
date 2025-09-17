using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_structure_demo
{
    internal class Student
    {
        // Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }

        // Default constructor
        public Student()
        {
            FirstName = "John";
            LastName = "Doe";
            Age = 18;
            Major = "Undeclared";
            GPA = 0.0;
        }

        // Parameterized constructor
        public Student(string firstName, string lastName, int age, string major, double gpa)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Major = major;
            GPA = gpa;
        }

        // Method to display student information
        public void DisplayStudentInfo()
        {
            Console.WriteLine("Name: "+this.FirstName+" "+this.LastName);
            Console.WriteLine("Age: "+this.Age);
            Console.WriteLine("Major "+ this.Major);
            Console.WriteLine("GPA: "+this.GPA);
        }
    }
}
