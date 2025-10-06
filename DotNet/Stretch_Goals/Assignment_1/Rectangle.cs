using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment_1
{
    internal class Rectangle
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
            length= Convert.ToDecimal(Console.ReadLine());
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

        public Rectangle Areasum(decimal area1, decimal area2)
        {
            Rectangle rect= new Rectangle();
            rect.area = area1 + area2;
            return rect;
        }
    }
}
