using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public interface IUniversityDbContextFactory
{
    IStudentDbContext CreateStudentDbContext();
    ILessonDbContext CreateLessonDbContext();
    IScoreDbContext CreateScoreDbContext();
}