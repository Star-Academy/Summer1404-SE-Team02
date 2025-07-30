using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class StudentContext : DbContext
{
    public StudentContext()
    {
    }

    public StudentContext(DbContextOptions<StudentContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Lesson> Lessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql("Host=127.0.0.1;Port=5432;Database=university;User Id=postgres;Password=myPassword;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Scores)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentNumber)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .HasMany(c => c.Scores)
            .WithOne(sc => sc.Lesson)
            .HasForeignKey(sc => sc.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
            .HasKey(sc => new { sc.StudentNumber, sc.LessonId });

        modelBuilder.Entity<Lesson>()
            .HasIndex(s => s.LessonName)
            .IsUnique();
    }
}
