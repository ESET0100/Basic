using System.Runtime.Intrinsics.X86;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {   /*
            Console.WriteLine("Enter the student details: ");
            Console.WriteLine("Enter the student name: ");
            string name = Console.ReadLine();
            //input the student marks in 3 subjects
               Console.WriteLine("enter the marks in subject1: ");
               decimal marks1=Convert.ToDecimal(Console.ReadLine());
               Console.WriteLine("enter the marks in subject1: ");
               decimal marks2 = Convert.ToDecimal(Console.ReadLine());
               Console.WriteLine("enter the marks in subject1: ");
               decimal marks3 = Convert.ToDecimal(Console.ReadLine());
            
               decimal avg=(marks1+marks2+marks3)/3;
               decimal total=marks1+marks2+marks3;
               Console.WriteLine("The details of student are: ");
               Console.WriteLine("Name: " + name + ", Total: " + total + ", Avg: " + avg); 
            decimal total=0, avg;
            //     decimal[] arr = {marks1 ,marks2 ,marks3 };
            decimal[] arr = { 50, 67, 72};
            for (int i = 0; i < arr.Length; i++)
            {
                total+=arr[i];

            }
            avg=total/arr.Length;
            Console.WriteLine("The details of student are: ");
            Console.WriteLine("Name: " + name + ", Total: " + total + ", Avg: " + avg);
            */
            
            void input(ref decimal marks1,ref decimal marks2,ref decimal marks3,ref string name)
            {
                Console.WriteLine("Enter the student name: ");
                name = Console.ReadLine();
                Console.WriteLine("enter the marks in subject 1: ");
                marks1 = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("enter the marks in subject 2: ");
                marks2 = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("enter the marks in subject 3: ");
                marks3 = Convert.ToDecimal(Console.ReadLine());
            }
            void display(decimal marks1, decimal marks2, decimal marks3, string name)
            {
                Console.WriteLine("The details of student are: ");
                Console.WriteLine("Name: " + name + ", Marks1: " + marks1 + ", Marks2: " + marks2 + ", Marks3: " + marks3);
            }
            void total(decimal marks1, decimal marks2, decimal marks3)
            {
                decimal total = marks1 + marks2 + marks3;
                Console.WriteLine("The total is: " + total);
            }
            void average(decimal marks1, decimal marks2, decimal marks3)
            {
                decimal avg = (marks1 + marks2 + marks3) / 3;
                Console.WriteLine("The Average is: " + avg);
            }
            
            Console.WriteLine("Enter the student details: ");
            decimal marks1=0, marks2=0, marks3=0;
            string name="";
            input(ref marks1,ref marks2,ref marks3,ref name);
            display(marks1, marks2, marks3, name);
            total(marks1, marks2, marks3);
            average(marks1, marks2, marks3); 
            
        }
        
    }
}
