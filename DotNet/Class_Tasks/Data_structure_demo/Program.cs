namespace Data_structure_demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create student objects using different constructors
            Student student1 = new Student(); // Default constructor
            Student student2 = new Student("Alice", "Johnson", 20, "Computer Science", 3.8); // Parameterized constructor
            Student student3 = new Student("Bob", "Smith", 22, "Mathematics", 3.5); // Parameterized constructor

            // Create a list to store student objects
            List<Student> students = new List<Student>();

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
                students[i].DisplayStudentInfo();
                Console.WriteLine("-----------------");
            }

            Console.ReadLine();
        }
    }
}
