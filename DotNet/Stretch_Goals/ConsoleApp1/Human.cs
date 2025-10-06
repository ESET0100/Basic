using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{   //GRANDPARENT CLASS
    internal class Human
    {
        string blood_group;
        int age;
        string New_name;

        //public Human() {
        //    blood_group = "";
        //    age = 0;
        //    New_name = "";
        
        //}
        public Human(string name) // parameterized constructor called using base keyword in derived class
        {
            New_name = name;
            blood_group = "";
            age = 0;
        }
        public void HumanInput()
        {
            Console.WriteLine("Enter the age: ");
            age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the blood_group: ");
            blood_group = Console.ReadLine();



        }

        public void HumanDisplay()
        {
            Console.WriteLine("The details of the human are: ");
            Console.WriteLine($"Blood Group: {blood_group}, Age: {age}, New name: {New_name}");

        }
    }
}
