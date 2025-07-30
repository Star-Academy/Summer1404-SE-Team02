using PH01___C__tutorial.DTO;

namespace PH01___C__tutorial;

public interface IAverageCalculator
{
    public List<AverageDto> CalculateAverageTop3(StudentContext studentContext);
}