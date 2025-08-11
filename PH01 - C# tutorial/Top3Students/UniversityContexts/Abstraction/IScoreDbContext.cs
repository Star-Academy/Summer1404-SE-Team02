using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial.UniversityContexts;

public class IScoreDbContext
{
    public DbSet<Score> Scores { get; set; }
}