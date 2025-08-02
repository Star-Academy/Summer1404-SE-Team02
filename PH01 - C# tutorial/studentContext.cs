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
}