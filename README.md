# Student Course Enrollment System

A console application built with C# and .NET that allows users to manage students and course enrollments.

## About the Project

I created this project to practice working with C# and object-oriented programming. The application allows users to enroll students in courses, update student information, and save enrollment records so that the information is still available the next time the program runs.

## Features

- View available courses and enrolled students
- Enroll students using unique student IDs
- Prevent duplicate enrollments
- Remove students from courses
- Update student names across courses
- Check for invalid user input
- Save and load enrollment information using JSON

## Technologies Used

- C#
- .NET 10
- LINQ
- JSON
- xUnit

## How to Run

Make sure the .NET 10 SDK is installed.

Clone the repository:

```bash
git clone https://github.com/lilysakuma/student-course-enrollment-system.git
cd student-course-enrollment-system
```

Run the application:

```bash
dotnet run --project CanvasConsole.csproj
```

## Saving Data

Student information and course enrollments are saved in a file called `enrollment.json` when the user exits through the main menu.

When the program is opened again from the same working directory, it loads the previously saved information.

## Testing

I added automated tests using xUnit to check that the enrollment system works as expected.

To run the tests:

```bash
dotnet test CanvasConsole.Tests/CanvasConsole.Tests.csproj
```

## What I Learned

This project helped me become more comfortable with C#, classes, objects, and working with collections. I also learned how to save and retrieve information using JSON instead of losing all the data when the program closes.

Adding automated tests helped me understand how to check that individual parts of a program work correctly.

## Future Improvements

- Add more course management options
- Improve error handling for damaged JSON files
- Expand automated test coverage
- Create a graphical user interface

## Author

Lily Sakuma  
Computer Science Student at Florida State University
