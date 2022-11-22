using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Security.Domain.Models;
using Menthos.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Questions
        builder.Entity<Question>().ToTable("Questions");
        builder.Entity<Question>().HasKey(p => p.Id);
        builder.Entity<Question>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Question>().Property(p => p.Content).IsRequired().HasMaxLength(300);
        
        // Relationships
        builder.Entity<Question>()
            .HasMany(p => p.Answers)
            .WithOne(p => p.Question)
            .HasForeignKey(p => p.QuestionId);

        // Answers
        builder.Entity<Answer>().ToTable("Answers");
        builder.Entity<Answer>().HasKey(p => p.Id);
        builder.Entity<Answer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Answer>().Property(p => p.Content).IsRequired();
        
        // Subjects
        builder.Entity<Subject>().ToTable("Subjects");
        builder.Entity<Subject>().HasKey(p => p.Id);
        builder.Entity<Subject>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Subject>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Subject>().Property(p => p.Image);
        
        builder.Entity<Subject>()
            .HasMany(p => p.Questions)
            .WithOne(p => p.Subject)
            .HasForeignKey(p => p.SubjectId);
        
        // Users
        
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

        builder.Entity<Student>()
            .HasMany(p => p.Questions)
            .WithOne(p => p.Student)
            .HasForeignKey(p => p.StudentId);
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }

}