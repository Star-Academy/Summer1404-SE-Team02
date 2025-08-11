using PH01___C__tutorial.DTO;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class AverageCalculator : IAverageCalculator
{
    private readonly IScoreDbContext _scoreDbContext;

    public AverageCalculator(IUniversityDbContextFactory dbContextFactory)
    {
        _scoreDbContext = dbContextFactory.CreateScoreDbContext();
    }
    public List<AverageDto> CalculateAverageTop3()
    {
        return _scoreDbContext.Scores
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