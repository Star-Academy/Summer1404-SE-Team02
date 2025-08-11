using PH01___C__tutorial.DTO;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public interface IAverageCalculator
{
    public List<AverageDto> CalculateAverageTop3();
}