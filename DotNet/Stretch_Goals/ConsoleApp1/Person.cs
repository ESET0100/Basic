using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{   //PARENT CLASS
    internal class Person : Human
    {
        long mobile_number;
        string gender;
        string address;

        public Person():base("New Name")  //base keyword - alternative of super()
        {
            mobile_number = 0;
            gender = "";
            address = "";
        }
        public void PersonInput()
        {
            Console.WriteLine("Enter the mobile number: ");
            mobile_number = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Enter the gender of the person: ");
            gender = Console.ReadLine();

            Console.WriteLine("Enter the address: ");
            address = Console.ReadLine();
        }

        public void PersonDisplay()
        {
            Console.WriteLine("The details of the person are: ");
            Console.WriteLine($"Mobile number: {mobile_number}, Gender: {gender}, Address: {address}");
        }

        public virtual void test() // Method Overriding
        {
            Console.WriteLine("I am in the Person class!");
        }
    }
}
