using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public interface IStudentDbContext : ICommonDbContext
{
    public DbSet<Student> Students { get; set; }
}