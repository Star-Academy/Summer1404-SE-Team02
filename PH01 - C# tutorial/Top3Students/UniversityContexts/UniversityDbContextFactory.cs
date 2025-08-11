using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public class UniversityDbContextFactory : IUniversityDbContextFactory
{
    private readonly IDbContextFactory<UniversityDbContext> _universityDbContextFactory;
    public UniversityDbContextFactory(IDbContextFactory<UniversityDbContext> universityDbContextFactory)
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

    public ICommonDbContext CreateCommonDbContext()
    {
        return _universityDbContextFactory.CreateDbContext();
    }
}