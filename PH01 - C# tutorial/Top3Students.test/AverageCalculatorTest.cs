using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using PH01___C__tutorial;
using PH01___C__tutorial.UniversityContexts;
using Xunit;

public class AverageCalculatorTests
{
    private readonly IUniversityDbContextFactory _dbContextFactory;
    private readonly IAverageCalculator _sut;
    public AverageCalculatorTests()
    {
        _dbContextFactory = Substitute.For<IUniversityDbContextFactory>();
        _sut = new AverageCalculator(_dbContextFactory);
    }
    private IScoreDbContext GetInMemoryStudentContext()
    {
        var options = new DbContextOptionsBuilder<UniversityDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
        _dbContextFactory.CreateScoreDbContext().Returns(new UniversityDbContext(new DbContextOptionsBuilder<UniversityDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options));
        
        var context = _dbContextFactory.CreateScoreDbContext();

        var scores = new List<Score>
        {
            new Score { StudentNumber = 1, LessonId = 101, Grade = 80 },
            new Score { StudentNumber = 1, LessonId = 102, Grade = 90 },
            new Score { StudentNumber = 1, LessonId = 103, Grade = 70 },

            new Score { StudentNumber = 2, LessonId = 101, Grade = 95 },
            new Score { StudentNumber = 2, LessonId = 102, Grade = 85 },
            new Score { StudentNumber = 2, LessonId = 103, Grade = 90 },

            new Score { StudentNumber = 3, LessonId = 101, Grade = 60 },
            new Score { StudentNumber = 3, LessonId = 102, Grade = 75 },
            new Score { StudentNumber = 3, LessonId = 103, Grade = 80 },

            new Score { StudentNumber = 4, LessonId = 101, Grade = 100 },
            new Score { StudentNumber = 4, LessonId = 102, Grade = 100 },
            new Score { StudentNumber = 4, LessonId = 103, Grade = 100 },
        };
        context.Scores.AddRange(scores);

        context.SaveChanges();

        return context;
    }

    [Fact]
    public void CalculateAverageTop3_Returns_Top3StudentsByAverageScore()
    {
        // Arrange
        var context = GetInMemoryStudentContext();
        _dbContextFactory.CreateScoreDbContext().Returns(context);
        var calculator = new AverageCalculator(_dbContextFactory);

        // Act
        var result = calculator.CalculateAverageTop3();

        // Assert
        Assert.Equal(3, result.Count);

        Assert.Equal(4, result[0].StudentNumber);
        Assert.Equal(2, result[1].StudentNumber);
        Assert.Equal(1, result[2].StudentNumber);
    }
}