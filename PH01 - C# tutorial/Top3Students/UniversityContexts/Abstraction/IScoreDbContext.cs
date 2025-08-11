using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public interface IScoreDbContext : ICommonDbContext
{
    public DbSet<Score> Scores { get; set; }
}