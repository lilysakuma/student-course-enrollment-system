namespace CanvasConsole.Models;
using System;
// ======================
// STUDENT CLASS
// ======================
// This represents ONE student in the entire system
public class Student
{
    // Unique ID for the student
    public int Id { get; }

    // Student's name
    public string Name { get; private set;} = string.Empty;

    // Constructor: runs when a new Student is created
    public Student(int id, string name)
    {
        Id = id;                   // Store the ID
        ChangeName(name);        // Store the name (trim removes extra spaces)
    }
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Name cannot be empty.");
        }

        Name = newName.Trim();
    }

    // This controls how the student prints when we Console.WriteLine(student)
    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}
