using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public class IStudentDbContext
{
    public DbSet<Student> Students { get; set; }
}