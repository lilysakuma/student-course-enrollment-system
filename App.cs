// We need these libraries to use things like Console, Lists, and LINQ
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using CanvasConsole.Models;
// ======================
// MAIN APPLICATION CLASS
// ======================
// This class controls the whole program flow
public class App
{
    // MASTER LIST of ALL students in the entire system
    // There should ONLY be ONE of these
    private readonly List<Student> _allStudents = new();

    // List of all courses
    private readonly List<Course> _courses = new();

    // Entry point for the app logic
    public void Run()
    {
        // Add starter courses if none exist
        LoadData();

        // Main program loop (runs until user exits)
        while (true)
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1) List courses");
            Console.WriteLine("2) Select course");
            Console.WriteLine("3) Update student name");
            Console.WriteLine("4) Exit");
            Console.Write("Choose: ");

            // Read user input
            string choice = Console.ReadLine()?.Trim() ?? "";

            // Decide what to do based on input
            if (choice == "1")
                ListCourses();
            else if (choice == "2")
                SelectCourseMenu();
            else if (choice =="3")
                UpdateStudentName();
            else if (choice == "4")
            {
                SaveData();
                return;
            }
            else
                Console.WriteLine("Invalid choice.");
        }
    }

    // Adds example courses so the app isn't empty
    private void SeedCourses()
    {
        if (_courses.Count == 0)
        {
            _courses.Add(new Course("COP4870"));
            _courses.Add(new Course("COP3330"));
        }
    }

    // Displays all available courses
    private void ListCourses()
    {
        Console.WriteLine("\nCourses:");
        for (int i = 0; i < _courses.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {_courses[i].Title}");
        }
    }

    // Lets the user choose a course
    private void SelectCourseMenu()
    {
        ListCourses();
        Console.Write("Enter course number: ");

        // Validate user input
        if (!int.TryParse(Console.ReadLine(), out int num) ||
            num < 1 || num > _courses.Count)
        {
            Console.WriteLine("Invalid course selection.");
            return;
        }

        // Convert number to course
        Course selected = _courses[num - 1];

        // Open the course menu
        CourseMenu(selected);
    }

    // Menu specific to a single course
    private void CourseMenu(Course course)
    {
        while (true)
        {
            Console.WriteLine($"\n=== COURSE MENU: {course.Title} ===");
            Console.WriteLine("1) View enrolled students");
            Console.WriteLine("2) Enroll student");
            Console.WriteLine("3) Unenroll student");
            Console.WriteLine("4) Back");
            Console.Write("Choose: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "1")
                ViewEnrolled(course);
            else if (choice == "2")
                EnrollStudent(course); // ISSUE #1 happens here
            else if (choice == "3")
                UnenrollStudent(course);
            else if (choice == "4")
                return;
            else
                Console.WriteLine("Invalid choice.");
        }
    }

    // Shows students enrolled in a course
    private void ViewEnrolled(Course course)
    {
        Console.WriteLine($"\nEnrolled students in {course.Title}:");

        if (course.EnrolledStudents.Count == 0)
        {
            Console.WriteLine("(none)");
            return;
        }

        foreach (Student s in course.EnrolledStudents)
        {
            Console.WriteLine($"- {s}");
        }
    }

    // ======================
    // ISSUE #1: ENROLL STUDENT
    // ======================
    private void EnrollStudent(Course course)
    {
        Console.Write("Enter student ID (number): ");

        // Validate ID input
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        // Look for student in MASTER list
        Student? student =
            _allStudents.FirstOrDefault(s => s.Id == id);

        // If student does NOT exist, create them
        if (student == null)
        {
            Console.Write("Student not found. Enter student name: ");
            string name = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            // Create new student ONCE
            student = new Student(id, name);

            // Add to MASTER list
            _allStudents.Add(student);
        }

        // Prevent enrolling the same student twice
        bool alreadyEnrolled =
            course.EnrolledStudents.Any(s => s.Id == student.Id);

        if (alreadyEnrolled)
        {
            Console.WriteLine("Student already enrolled in this course.");
            return;
        }

        // SHALLOW COPY:
        // We add the SAME student reference to the course
        course.EnrolledStudents.Add(student);

        Console.WriteLine(
            $"Enrolled {student.Name} (ID {student.Id}) in {course.Title}."
        );
    }
    //Unenroll student
    private void UnenrollStudent( Course course)
    {
       Console.Write("Enter student ID to unenroll: ");
        string input = Console.ReadLine() ?? "";
        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        Student? student = course.EnrolledStudents.FirstOrDefault(s => s.Id ==id);
        if(student == null)
        {
            Console.WriteLine("Student is not enrolled in this course.");
            return;
        }
        course.EnrolledStudents.Remove(student);
        Console.WriteLine($"{student.Name} was successfully unenrolled.");

    }
    private void UpdateStudentName()
    {
        Console.Write("Enter student ID to update Name:");
        string input = Console.ReadLine() ?? "";
        if(!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }
        Student? student = _allStudents.FirstOrDefault(s=>s.Id==id);
        if(student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }
        Console.WriteLine("Enter new student Name: ");
        string newName = Console.ReadLine() ?? "";
        if(string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Invalid Name.");
            return;
        }
        student.ChangeName(newName);
        Console.WriteLine("Student name updated successfully.");


    }
        private void SaveData()
    {
        EnrollmentData data = new EnrollmentData();

        foreach (Student student in _allStudents)
        {
            data.Students.Add(new StudentData
            {
                Id = student.Id,
                Name = student.Name
            });
        }

        foreach (Course course in _courses)
        {
            data.Courses.Add(new CourseData
            {
                Title = course.Title,
                StudentIds = course.EnrolledStudents
                    .Select(s => s.Id)
                    .ToList()
            });
        }

        string json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText("enrollment.json", json);
    }
        private void LoadData()
    {
        if (!File.Exists("enrollment.json"))
        {
            SeedCourses();
            return;
        }

        string json = File.ReadAllText("enrollment.json");

        EnrollmentData? data =
            JsonSerializer.Deserialize<EnrollmentData>(json);

        if (data == null)
        {
            SeedCourses();
            return;
        }

        _allStudents.Clear();
        _courses.Clear();

        foreach (StudentData studentData in data.Students)
        {
            Student student = new Student(
                studentData.Id,
                studentData.Name
            );

            _allStudents.Add(student);
        }

        foreach (CourseData courseData in data.Courses)
        {
            Course course = new Course(courseData.Title);

            foreach (int studentId in courseData.StudentIds)
            {
                Student? student = _allStudents
                    .FirstOrDefault(s => s.Id == studentId);

                if (student != null)
                {
                    course.EnrolledStudents.Add(student);
                }
            }

            _courses.Add(course);
        }
    }

}
