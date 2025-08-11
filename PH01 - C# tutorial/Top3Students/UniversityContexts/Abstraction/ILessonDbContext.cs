using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public class ILessonDbContext
{
    public DbSet<Lesson> Lessons { get; set; }
}