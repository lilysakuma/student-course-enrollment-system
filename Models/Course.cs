namespace CanvasConsole.Models;
// ======================
// COURSE CLASS
// ======================
// This represents ONE course (like COP4870)
public class Course
{
    // Name/title of the course
    public string Title { get; }

    // List of students enrolled in THIS course
    // IMPORTANT: These are references to students from the MASTER list
    public List<Student> EnrolledStudents { get; } = new();

    // Constructor: runs when a new Course is created
    public Course(string title)
    {
        Title = title.Trim();
    }
}
