namespace MvcExe.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string Github { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime Created { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
}