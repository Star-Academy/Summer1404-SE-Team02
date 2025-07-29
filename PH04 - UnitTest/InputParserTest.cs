using Xunit;
using System.Collections.Generic;
using InvertedIndexIR.InputParser;

public class InputParserTests
{
    private readonly InputParser _parser = new InputParser();
    private const string DefaultPattern = @"([+-]?""[^""]+""|[+-]?\S+)";

    [Fact]
    public void Should_Parse_Simple_Unquoted_Terms()
    {
        var input = "apple banana orange";
        var expected = new List<string> { "APPLE", "BANANA", "ORANGE" };

        var result = _parser.ParseInput(input, DefaultPattern);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Parse_Quoted_Terms_With_Prefix()
    {
        var input = "+\"green apple\" -\"red banana\"";
        var expected = new List<string> { "+GREEN APPLE", "-RED BANANA" };

        var result = _parser.ParseInput(input, DefaultPattern);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Parse_Mixed_Input()
    {
        var input = "apple +fruit -banana +\"green apple\"";
        var expected = new List<string> { "APPLE", "+FRUIT", "-BANANA", "+GREEN APPLE" };

        var result = _parser.ParseInput(input, DefaultPattern);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Handle_Empty_Input()
    {
        var input = "";
        var result = _parser.ParseInput(input, DefaultPattern);

        Assert.Empty(result);
    }

    [Fact]
    public void Should_Strip_Quotes_Only_When_Both_Sides_Quoted()
    {
        var input = "\"apple banana\" orange";
        var expected = new List<string> { "APPLE BANANA", "ORANGE" };

        var result = _parser.ParseInput(input, DefaultPattern);

        Assert.Equal(expected, result);
    }
}
