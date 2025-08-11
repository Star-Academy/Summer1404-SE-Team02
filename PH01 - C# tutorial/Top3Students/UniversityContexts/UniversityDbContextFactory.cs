using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public class DbContextFactory : IUniversityDbContextFactory
{
    private readonly IDbContextFactory<UniversityDbContext> _universityDbContextFactory;
    public DbContextFactory(IDbContextFactory<UniversityDbContext> universityDbContextFactory)
    {
        _universityDbContextFactory = universityDbContextFactory ?? throw new ArgumentNullException(nameof(universityDbContextFactory));
    }

    public IStudentDbContext CreateStudentDbContext()
    {
        return _universityDbContextFactory.CreateDbContext();
    }

    public ILessonDbContext CreateLessonDbContext()
    {
        return _universityDbContextFactory.CreateDbContext();
    }
    
    public IScoreDbContext CreateScoreDbContext()
    {
        return _universityDbContextFactory.CreateDbContext();
    }
}