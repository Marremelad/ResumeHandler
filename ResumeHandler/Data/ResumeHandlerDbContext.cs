using Microsoft.EntityFrameworkCore;
using ResumeHandler.Models;

namespace ResumeHandler.Data;

public class ResumeHandlerDbContext(DbContextOptions<ResumeHandlerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Education> Educations { get; set; }

    public DbSet<WorkExperience> WorkExperiences { get; set; }
}