namespace ExeApp.Models;

public class Project {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string GithubLink { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedAt { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public string? IdentityUserId { get; set; }
    public IdentityUser? IdentityUser { get; set; }
}