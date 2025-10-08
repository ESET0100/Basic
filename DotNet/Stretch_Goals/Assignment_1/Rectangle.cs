using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment_1
{
    internal class Rectangle : IShape
    {
        public decimal length, breadth, area;

        public Rectangle()
        {
            length = 0;
            breadth = 0;
            area = 0;
        }

        public void input()
        {
            Console.WriteLine("Enter the length of rectangle: ");
            length = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter the breadth of rectangle: ");
            breadth = Convert.ToDecimal(Console.ReadLine());
        }

        public void Area()
        {
            decimal ar = length * breadth;
            area = ar;
        }

        public void display()
        {
            Console.WriteLine($"Length: {length}, Breadth: {breadth}");
            Console.WriteLine("The area of rectangle is: " + area);
        }

        // Interface implementation - CalculateArea method
        public decimal CalculateArea()
        {
            return length * breadth;
        }

        // Interface implementation - DisplayInfo method
        public void DisplayInfo()
        {
            Console.WriteLine($"Rectangle - Length: {length}, Breadth: {breadth}, Area: {area}");
        }

        // Operator overloading for + operator
        public static Rectangle operator +(Rectangle rect1, Rectangle rect2)
        {
            Rectangle result = new Rectangle();
            result.area = rect1.area + rect2.area;
            return result;
        }
    }
}