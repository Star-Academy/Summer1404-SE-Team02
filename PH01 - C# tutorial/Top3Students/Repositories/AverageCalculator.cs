using PH01___C__tutorial.DTO;

namespace PH01___C__tutorial;

public class AverageCalculator : IAverageCalculator
{
    public List<AverageDto> CalculateAverageTop3(StudentContext stContext)
    {
        return stContext.Scores
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