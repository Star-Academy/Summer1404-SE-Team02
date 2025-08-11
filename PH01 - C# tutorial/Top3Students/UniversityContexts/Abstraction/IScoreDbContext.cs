using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public interface IScoreDbContext
{
    public DbSet<Score> Scores { get; set; }
}