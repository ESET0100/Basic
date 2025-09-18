using System;
using System.Collections.Generic;

// Student class definition
public class Student
{
    public int id;
    public string name;
    public int marks;

    // Constructor
    public Student(int id, string name, int marks)
    {
        this.id = id;
        this.name = name;
        this.marks = marks;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== STUDENT MANAGEMENT PROGRAM ===");

        // Create student objects
        Student first = new Student(1, "Alice", 85);
        Student second = new Student(2, "Bob", 92);
        Student third = new Student(3, "Charlie", 78);
        Student fourth = new Student(4, "Diana", 88);

        // 1. List Example
        Console.WriteLine("\n1. LIST EXAMPLE:");
        List<Student> studentsList = new List<Student>();
        studentsList.Add(first);
        studentsList.Add(second);
        studentsList.Add(third);

        Console.WriteLine("All students in list:");
        foreach (Student s in studentsList)
        {
            Console.WriteLine("ID: " + s.id + ", Name: " + s.name + ", Marks: " + s.marks);
        }

        // 2. Dictionary Example
        Console.WriteLine("\n2. DICTIONARY EXAMPLE:");
        Dictionary<string, Student> studentsDict = new Dictionary<string, Student>();
        studentsDict.Add("first", first);
        studentsDict.Add("second", second);
        studentsDict.Add("third", third);

        foreach (KeyValuePair<string, Student> entry in studentsDict)
        {
            Console.WriteLine("Key: " + entry.Key + " -> Name: " + entry.Value.name);
        }

        // 3. HashSet Example
        Console.WriteLine("\n3. HASHSET EXAMPLE:");
        HashSet<Student> studentsHashSet = new HashSet<Student>();
        studentsHashSet.Add(first);
        studentsHashSet.Add(second);
        studentsHashSet.Add(first); // Duplicated wont be added in the HashSet

        Console.WriteLine("Students in HashSet:");
        foreach (Student s in studentsHashSet)
        {
            Console.WriteLine("ID: " + s.id + ", Name: " + s.name);
        }

        // 4. Stack Example
        Console.WriteLine("\n4. STACK EXAMPLE:");
        Stack<Student> studentsStack = new Stack<Student>();
        studentsStack.Push(first);
        studentsStack.Push(second);
        studentsStack.Push(third);

        Student poppedStudent = studentsStack.Pop();
        Console.WriteLine("Popped from stack: " + poppedStudent.name);

        // 5. Queue Example
        Console.WriteLine("\n5. QUEUE EXAMPLE:");
        Queue<Student> studentsQueue = new Queue<Student>();
        studentsQueue.Enqueue(first);
        studentsQueue.Enqueue(second);
        studentsQueue.Enqueue(third);

        Student dequeuedStudent = studentsQueue.Dequeue();
        Console.WriteLine("Dequeued from queue: " + dequeuedStudent.name);

        // 6. Tuple Example
        Console.WriteLine("\n6. TUPLE EXAMPLE:");
        // Create tuples with student information
        var studentTuple1 = (first.id, first.name, first.marks);
        var studentTuple2 = (second.id, second.name, second.marks);
        var studentTuple3 = (third.id, third.name, third.marks);

        // Store tuples in a list
        List<ValueTuple<int, string, int>> tupleList = new List<ValueTuple<int, string, int>>();
        tupleList.Add(studentTuple1);
        tupleList.Add(studentTuple2);
        tupleList.Add(studentTuple3);

        Console.WriteLine("Students as tuples:");
        foreach (var tuple in tupleList)
        {
            Console.WriteLine("ID: " + tuple.Item1 + ", Name: " + tuple.Item2 + ", Marks: " + tuple.Item3);
        }

        // Access tuple elements directly
        Console.WriteLine("First student from tuple: " + studentTuple1.name + " with marks: " + studentTuple1.marks);

        // 7. LinkedList Example
        Console.WriteLine("\n7. LINKED LIST EXAMPLE:");
        LinkedList<Student> studentsLinkedList = new LinkedList<Student>();
        studentsLinkedList.AddLast(first);
        studentsLinkedList.AddLast(second);
        studentsLinkedList.AddLast(third);
        studentsLinkedList.AddLast(fourth);

        Console.WriteLine("Students in linked list:");
        foreach (Student s in studentsLinkedList)
        {
            Console.WriteLine("ID: " + s.id + ", Name: " + s.name);
        }

        // Access specific nodes
        Console.WriteLine("First student: " + studentsLinkedList.First.Value.name);
        Console.WriteLine("Last student: " + studentsLinkedList.Last.Value.name);

        // Add student after a specific node
        Student fifth = new Student(5, "Eva", 95);
        studentsLinkedList.AddAfter(studentsLinkedList.First, fifth);

        Console.WriteLine("After adding new student:");
        foreach (Student s in studentsLinkedList)
        {
            Console.WriteLine("ID: " + s.id + ", Name: " + s.name);
        }

    
    }
}