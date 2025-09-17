using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create dictionaries for each student
        Dictionary<string, object> student1 = CreateStudent("John", "Doe", 18, "Undeclared", 0.0);
        Dictionary<string, object> student2 = CreateStudent("Alice", "Johnson", 20, "Computer Science", 3.8);
        Dictionary<string, object> student3 = CreateStudent("Bob", "Smith", 22, "Mathematics", 3.5);

        // Create a list to store student dictionaries
        List<Dictionary<string, object>> students = new List<Dictionary<string, object>>();

        // Add students to the list
        students.Add(student1);
        students.Add(student2);
        students.Add(student3);

        // Display all students in the list
        Console.WriteLine("List of Students:");
        Console.WriteLine("=================");

        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"Student {i + 1}:");
            DisplayStudentInfo(students[i]);
            Console.WriteLine("-----------------");
        }

        Console.ReadLine();
    }

    // Method to create a student dictionary
    static Dictionary<string, object> CreateStudent(string firstName, string lastName, int age, string major, double gpa)
    {
        return new Dictionary<string, object>
        {
            { "FirstName", firstName },
            { "LastName", lastName },
            { "Age", age },
            { "Major", major },
            { "GPA", gpa }
        };
    }

    // Method to create a default student using default constructor equivalent
    static Dictionary<string, object> CreateDefaultStudent()
    {
        return CreateStudent("John", "Doe", 18, "Undeclared", 0.0);
    }

    // Method to display student information
    static void DisplayStudentInfo(Dictionary<string, object> student)
    {
        Console.WriteLine($"Name: {student["FirstName"]} {student["LastName"]}");
        Console.WriteLine($"Age: {student["Age"]}");
        Console.WriteLine($"Major: {student["Major"]}");
        Console.WriteLine($"GPA: {Convert.ToDouble(student["GPA"]):F2}");
    }
}