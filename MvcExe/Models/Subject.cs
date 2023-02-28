namespace MvcExe.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public int Semester { get; set; }

    public List<Project> Projects { get; set; }
}