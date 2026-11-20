using Xunit;
using CanvasConsole.Models;
public class StudentTests
{
    [Fact]
    public void ChangeName_ValidName_UpdatesStudentName()
    {
        Student student = new Student(101, "Alex");

        student.ChangeName("Alexander");

        Assert.Equal("Alexander", student.Name);
    }
    [Fact]
    public void ChangeName_EmptyName_ThrowsException()
    {
        Student student = new Student(101, "Alex");

        Assert.Throws<ArgumentException>(() => student.ChangeName(""));
    }
    [Fact]
    public void ChangeName_EmptyName_KeepsOriginalName()
    {
        Student student = new Student(101, "Alex");

        Assert.Throws<ArgumentException>(() => student.ChangeName(""));

        Assert.Equal("Alex", student.Name);
    }
}