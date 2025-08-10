using System.Collections.Generic;
using FluentAssertions;
using InvertedIndexIR.QueryBuilder;
using InvertedIndexIR.QueryBuilder.Abstraction;

namespace InvertedIndexTests;

public class QueryBuilderTests
{
    private readonly IQueryBuilder _sut;
    public QueryBuilderTests()
    {
        _sut = new QueryBuilder();
    }

    [Fact]
    public void QueryBuilder_BuildQuery_BuildsCorrectQuery()
    {
        // Arrange
        var input = new List<string> {"+cat", "-dog", "fish"};
        var notations = new List<string> { "+", "-"};

        // Act
        var result = _sut.BuildQuery(input, notations);

        // Assert
        result.ParsedWords["+"].Should().BeEquivalentTo(new List<string> { "CAT" });
        result.ParsedWords["-"].Should().BeEquivalentTo(new List<string> { "DOG" });
        result.ParsedWords[""].Should().BeEquivalentTo(new List<string> { "FISH" });
    }

    [Fact]
    public void QueryBuilder_BuildQuery_PutsUnprefixedWordsInDefaultKey()
    {
        // Arrange
        var input = new List<string> {"fish"};
        var notations = new List<string> { "+", "-"};

        // Act
        var result = _sut.BuildQuery(input, notations);

        // Assert
        result.ParsedWords[""].Should().BeEquivalentTo(new List<string> { "FISH" });
        result.ParsedWords["+"].Should().BeEmpty();
        result.ParsedWords["-"].Should().BeEmpty();
    }
    
    [Fact]
    public void QueryBuilder_BuildQuery_Puts_Phrases()
    {
        // Arrange
        var input = new List<string> {"+fish and chips", "fish"};
        var notations = new List<string> { "+", "-"};

        // Act
        var result = _sut.BuildQuery(input, notations);

        // Assert
        result.ParsedWords["+"].Should().BeEquivalentTo(new List<string> { "FISH AND CHIPS" });
        result.ParsedWords[""].Should().BeEquivalentTo(new List<string> { "FISH" });
    }
}