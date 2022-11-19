using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Security.Domain.Models;
using Menthos.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Questions
        builder.Entity<Question>().ToTable("Questions");
        builder.Entity<Question>().HasKey(p => p.Id);
        builder.Entity<Question>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Question>().Property(p => p.Content).IsRequired().HasMaxLength(300);

        //Users
        
        //Teachers
        builder.Entity<Teacher>().ToTable("Teachers");
        builder.Entity<Teacher>().HasKey(p => p.Id);
        builder.Entity<Teacher>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Teacher>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Teacher>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
        builder.Entity<Teacher>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<Teacher>().Property(p => p.Codigo).IsRequired().HasMaxLength(10);
        builder.Entity<Teacher>().Property(p => p.email).IsRequired().HasMaxLength(30);
        builder.Entity<Teacher>().Property(p => p.telephone).IsRequired().HasMaxLength(9);

        //Students
        builder.Entity<Student>().ToTable("Students");
        builder.Entity<Student>().HasKey(p => p.Id);
        builder.Entity<Student>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Student>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Student>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
        builder.Entity<Student>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<Student>().Property(p => p.Codigo).IsRequired().HasMaxLength(10);
        builder.Entity<Student>().Property(p => p.email).IsRequired().HasMaxLength(30);
        builder.Entity<Student>().Property(p => p.telephone).IsRequired().HasMaxLength(9);
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }

}