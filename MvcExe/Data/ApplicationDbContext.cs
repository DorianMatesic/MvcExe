using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcExe.Models;

namespace MvcExe.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<MvcExe.Models.Subject> Subjects { get; set; } = default!;
    public DbSet<MvcExe.Models.Project> Projects { get; set; }
}
