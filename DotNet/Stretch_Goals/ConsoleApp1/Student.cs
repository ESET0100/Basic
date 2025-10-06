using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{   //CHILD CLASS
    internal class Student: Person
    {
        string Name;
        decimal Marks1, Marks2, Marks3;

        public Student(decimal Marks1, decimal Marks2, decimal Marks3, string Name)
        {
            this.Name = Name;  //this operator
            this.Marks1 = Marks1;
            this.Marks2 = Marks2;
            this.Marks3 = Marks3;
        }

        public Student() {
            Name = "";
            Marks1 = 0;
            Marks2 = 0;
            Marks3 = 0;
        }

        public void Input()
        {
            Console.WriteLine("Enter the student name: ");
            Name = Console.ReadLine();

            Console.WriteLine("Enter the marks in subject 1: ");
            Marks1 = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter the marks in subject 2: ");
            Marks2 = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter the marks in subject 3: ");
            Marks3 = Convert.ToDecimal(Console.ReadLine());
        }

        public void Display()
        {
            Console.WriteLine("The details of student are: ");
            Console.WriteLine($"Name: {Name}, Marks1: {Marks1}, Marks2: {Marks2}, Marks3: {Marks3}");
        }

        public void Total()
        {
            decimal total=Marks1 + Marks2 + Marks3;
            Console.WriteLine("The total is: " + total);
        }

        public void Average()
        {
            decimal avg= (Marks1 + Marks2 + Marks3)/3;
            Console.WriteLine("The Average is: " + avg);
        }

        public override void test()  // Method Overriding
        {
            Console.WriteLine("I am in the Student class!");
        }

    }
}
