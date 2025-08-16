using PH01___C__tutorial.DTO;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class AverageCalculator : IAverageCalculator
{
    private readonly IUniversityDbContextFactory _dbContextFactory;

    public AverageCalculator(IUniversityDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public List<AverageDto> CalculateAverageTop3()
    {
        return _dbContextFactory.CreateScoreDbContext().Scores
            .GroupBy(item => item.StudentNumber)
            .Select(g => new AverageDto()
            {
                StudentNumber = g.Key,
                AverageScore = g.Average(item => item.Grade)
            })
            .OrderByDescending(item => item.AverageScore)
            .Take(3).ToList();
    }
}