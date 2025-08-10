using System.Collections.Generic;
using FluentAssertions;
using InvertedIndexIR.DTO;
using InvertedIndexIR.QueryGetWordsOfType;
using Xunit;
namespace InvertedIndexIR.Tests;

public class QueryWordsOfTypeGetterTests
{
    private readonly QueryWordsOfTypeGetter _sut;

    public QueryWordsOfTypeGetterTests()
    {
        _sut = new QueryWordsOfTypeGetter();
    }

    [Fact]
    public void GetWordsOfType_ShouldReturnList_WhenNotationExists()
    {
        // Arrange
        var query = new Query
        {
            ParsedWords = new Dictionary<string, List<string>>
            {
                { "+", new List<string> { "apple", "banana" } }
            }
        };

        // Act
        var result = _sut.GetWordsOfType(query, "+");

        // Assert
        result.Should().BeEquivalentTo(new List<string> { "apple", "banana" });
    }

    [Fact]
    public void GetWordsOfType_ShouldReturnEmptyList_WhenNotationDoesNotExist()
    {
        // Arrange
        var query = new Query
        {
            ParsedWords = new Dictionary<string, List<string>>
            {
                { "-", new List<string> { "cat" } }
            }
        };

        // Act
        var result = _sut.GetWordsOfType(query, "+");

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void GetWordsOfType_ReturnsCorrectWords_ForMultipleKeys()
    {
        // Arrange
        var query = new Query
        {
            ParsedWords = new Dictionary<string, List<string>>
            {
                { "+", new List<string> { "apple" } },
                { "-", new List<string> { "banana", "orange" } },
                { "", new List<string> { "grape" } }
            }
        };

        // Act
        var defaultWords = _sut.GetWordsOfType(query, "");
        var plusWords = _sut.GetWordsOfType(query, "+");
        var minusWords = _sut.GetWordsOfType(query, "-");

        // Assert
        defaultWords.Should().ContainSingle(x => x == "grape");
        plusWords.Should().ContainSingle(x => x == "apple");
        minusWords.Should().BeEquivalentTo(new List<string> { "banana", "orange" });
    }
}
