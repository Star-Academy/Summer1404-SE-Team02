using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Moq;
using InvertedIndexIR.InputParser;

namespace InvertedIndexIR.Tests
{
    public class InputParserTests
    {
        [Fact]
        public void ParseInput_ReturnsCorrectDictionary_ForSimpleInput()
        {
            // Arrange
            var parser = new InputParser.InputParser();
            string input = "+cat -dog fish";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
            var notations = new List<string> { "+", "-"};

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            result["+"].Should().BeEquivalentTo(new List<string> { "CAT" });
            result["-"].Should().BeEquivalentTo(new List<string> { "DOG" });
            result[""].Should().BeEquivalentTo(new List<string> { "FISH" });

        }

        [Fact]
        public void ParseInput_PutsUnprefixedWordsInDefaultKey()
        {
            // Arrange
            var parser = new InputParser.InputParser();
            string input = "apple \"banana is good\" -orange";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
            var notations = new List<string> { "+", "-"};

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            result["-"].Should().BeEquivalentTo(new List<string> { "ORANGE" });
            result[""].Should().BeEquivalentTo(new List<string> { "APPLE", "BANANA IS GOOD" });

        }

        [Fact]
        public void ParseInput_RemovesSurroundingQuotes()
        {
            // Arrange
            var parser = new InputParser.InputParser();
            string input = "+\"cat\" -\"dog\" \"something\"";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
            var notations = new List<string> { "+", "-" };

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            result["+"].Should().BeEquivalentTo(new List<string> { "CAT" });
            result["-"].Should().BeEquivalentTo(new List<string> { "DOG" });
            result[""].Should().BeEquivalentTo(new List<string> { "SOMETHING" });

        }

        [Fact]
        public void ParseInput_ReturnsEmptyLists_WhenNoMatches()
        {
            // Arrange
            var parser = new InputParser.InputParser();
            string input = "12345 67890";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";  // Won't match numbers
            var notations = new List<string> { "+", "-" };

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            result["+"].Should().BeEmpty();
            result["-"].Should().BeEmpty();
        }
    }
}
