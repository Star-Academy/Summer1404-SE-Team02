using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql($"Host=127.0.0.1;Port=5432;Database=university;User Id=postgres;Password=myPassword;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-many relationship: Student -> Scores
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Scores)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentNumber)
            .OnDelete(DeleteBehavior.Cascade);

        // One-to-many relationship: Course -> Scores
        modelBuilder.Entity<Lesson>()
            .HasMany(c => c.Scores)
            .WithOne(sc => sc.Lesson)
            .HasForeignKey(sc => sc.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Score>()
            .HasKey(sc => new { sc.StudentNumber, sc.LessonId });
    }
}