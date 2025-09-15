using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_demo
{
    internal class student
    {
        int studentId;
        string studentName;
        int age;
        string email;
        string contact;
        
        public void initialize()
        {
            studentId = 10;
            studentName = "John";
            age = 22;

        }
        public void display()
        {
            Console.WriteLine("Student id: "+ this.studentId);
            Console.WriteLine("Student name: "+ this.studentName);
        }
        public student(int studentId, string studentName, int age, string email, string contact)
        {
            this.studentId = studentId;
            this.studentName = studentName;
            this.age = age;
            this.email = email;
            this.contact = contact;
        }
    }
}
