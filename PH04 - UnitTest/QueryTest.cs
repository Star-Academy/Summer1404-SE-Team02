using Xunit;
using Moq;
using System.Collections.Generic;
using InvertedIndexIR.InputParser; // your real namespace
using InvertedIndexIR;             // assuming Query is in this namespace

public class QueryTests
{
    [Fact]
    public void GetWordsOfType_Returns_Matching_Notation_Prefixes()
    {
        // Arrange
        var mockParser = new Mock<InputParser>();
        string rawInput = "test input";
        string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
        var mockParsedWords = new List<string> { "APPLE", "+FRUIT", "-BANANA", "+GREEN APPLE" };

        mockParser.Setup(p => p.ParseInput(rawInput, pattern))
                  .Returns(mockParsedWords);

        var query = new Query(mockParser.Object, rawInput, pattern);

        // Act
        var result = query.GetWordsOfType("+");

        // Assert
        var expected = new List<string> { "+FRUIT", "+GREEN APPLE" };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetWordsOfType_Returns_Empty_When_No_Match()
    {
        // Arrange
        var mockParser = new Mock<InputParser>();
        string rawInput = "test input";
        string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
        var mockParsedWords = new List<string> { "APPLE", "BANANA" };

        mockParser.Setup(p => p.ParseInput(rawInput, pattern))
                  .Returns(mockParsedWords);

        var query = new Query(mockParser.Object, rawInput, pattern);

        // Act
        var result = query.GetWordsOfType("-");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetWordsOfType_Calls_ParseInput_Once()
    {
        // Arrange
        var mockParser = new Mock<InputParser>();
        string rawInput = "some query";
        string pattern = "some pattern";
        mockParser.Setup(p => p.ParseInput(rawInput, pattern)).Returns(new List<string>());

        var query = new Query(mockParser.Object, rawInput, pattern);

        // Act
        query.GetWordsOfType("+");

        // Assert
        mockParser.Verify(p => p.ParseInput(rawInput, pattern), Times.Once);
    }
}
