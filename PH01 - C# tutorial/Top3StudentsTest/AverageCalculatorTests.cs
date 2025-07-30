using Xunit;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Collections.Generic;
using PH01___C__tutorial;

public class AverageCalculatorTests
{
    [Fact]
    public void CalculateAverageTop3_ShouldReturnTop3StudentsSortedByAverageDescending()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<StudentContext>()
            .UseInMemoryDatabase(databaseName: "AverageTop3TestDb")
            .Options;

        using (var context = new StudentContext(options))
        {
            context.Scores.AddRange(
                new Score { StudentNumber = 1, LessonId = 101, Grade = 18 },
                new Score { StudentNumber = 1, LessonId = 102, Grade = 19 },
                new Score { StudentNumber = 2, LessonId = 101, Grade = 15 },
                new Score { StudentNumber = 2, LessonId = 102, Grade = 16 },
                new Score { StudentNumber = 3, LessonId = 101, Grade = 20 },
                new Score { StudentNumber = 4, LessonId = 101, Grade = 12 }
            );
            context.SaveChanges();

            var calculator = new AverageCalculator();

            // Act
            var result = calculator.CalculateAverageTop3(context);

            // Assert
            result.Count.ShouldBe(3);

            result[0].StudentNumber.ShouldBe(3); // Highest avg: 20
            result[1].StudentNumber.ShouldBe(1); // Avg: (18+19)/2 = 18.5
            result[2].StudentNumber.ShouldBe(2); // Avg: (15+16)/2 = 15.5

            result[0].AverageScore.ShouldBe(20);
            result[1].AverageScore.ShouldBe(18.5);
            result[2].AverageScore.ShouldBe(15.5);
        }
    }
}