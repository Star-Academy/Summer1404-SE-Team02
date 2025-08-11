using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public interface ILessonDbContext
{
    public DbSet<Lesson> Lessons { get; set; }
}