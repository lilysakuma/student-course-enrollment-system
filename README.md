# Student Course Enrollment System

A C# console application for managing student enrollment across multiple courses.

## Features

- View available courses and enrolled students
- Enroll students using unique student IDs
- Prevent duplicate enrollments
- Unenroll students from individual courses
- Update student names across courses
- Validate student information and user input
- Save and load enrollment records using JSON

## Technologies

- C#
- .NET 10
- LINQ
- System.Text.Json
- xUnit for unit testing

## Getting Started

Requires the .NET 10 SDK.

From the repository root, run:

```bash
dotnet run --project CanvasConsole.csproj
```

## Running Tests

```bash
dotnet test CanvasConsole.Tests/CanvasConsole.Tests.csproj
```

## Data Persistence

Student information and course enrollments are saved to `enrollment.json` when the user exits through the main menu.

The application restores saved records when restarted using the same working directory.

## Project Structure

- `CanvasConsole/Models/` — Student and Course models
- `CanvasConsole/Data/` — JSON data-transfer classes
- `CanvasConsole/App.cs` — Application menus and operations
- `CanvasConsole/Program.cs` — Application entry point
- `CanvasConsole.Tests/` — Automated unit tests

## Future Improvements

- More comprehensive automated tests
- Improved handling of invalid or corrupted JSON files
- Additional course-management functionality
