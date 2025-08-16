using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
        
       var context = new UniversityDbContext(options);

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
        _dbContextFactory.CreateScoreDbContext().Returns(GetInMemoryStudentContext());

        // Act
        var result = _sut.CalculateAverageTop3();

        // Assert
        result.Should().HaveCount(3);
        result.Select(r => r.StudentNumber).Should().Equal(4, 2, 1);
        result.Select(r => r.AverageScore).Should().Equal(100.0, 90.0, 80.0);
    }
}