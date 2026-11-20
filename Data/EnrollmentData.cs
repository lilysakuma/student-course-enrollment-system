using System.Collections.Generic;

public class EnrollmentData
{
    public List<StudentData> Students { get; set; } = new();
    public List<CourseData> Courses { get; set; } = new();
}

public class StudentData
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class CourseData
{
    public string Title { get; set; } = "";
    public List<int> StudentIds { get; set; } = new();
}