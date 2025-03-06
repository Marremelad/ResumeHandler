using Microsoft.EntityFrameworkCore;
using ResumeHandler.Models;

namespace ResumeHandler.Data;

public class ResumeHandlerDbContext : DbContext
{
    public ResumeHandlerDbContext(DbContextOptions options) :base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }

    public DbSet<ContactInformation> ContactInformation { get; set; }

    public DbSet<Education> Educations { get; set; }

    public DbSet<WorkExperience> WorkExperiences { get; set; }
}