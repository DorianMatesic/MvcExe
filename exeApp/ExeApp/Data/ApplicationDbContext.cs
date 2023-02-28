using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExeApp.Models;

namespace ExeApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ExeApp.Models.Subject> Subjects { get; set; }
    public DbSet<ExeApp.Models.Project> Projects { get; set; }
}
