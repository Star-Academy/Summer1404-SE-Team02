using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;


namespace InvertedIndexIR.Tests
{
    public class InputParserTests
    {
        [Fact]
        public void ParseInput_ReturnsCorrectDictionary_ForSimpleInput()
        {
            // Arrange
            var parser = new InputParser();
            string input = "+cat -dog fish";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
            var notations = new List<string> { "+", "-"};

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            Assert.Equal(new List<string> { "CAT" }, result["+"]);
            Assert.Equal(new List<string> { "DOG" }, result["-"]);
            Assert.Equal(new List<string> { "FISH" }, result[""]);
        }

        [Fact]
        public void ParseInput_PutsUnprefixedWordsInDefaultKey()
        {
            // Arrange
            var parser = new InputParser();
            string input = "apple \"banana is good\" -orange";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
            var notations = new List<string> { "+", "-"};

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            Assert.Equal(new List<string> { "APPLE", "BANANA IS GOOD" }, result[""]);
            Assert.Equal(new List<string> { "ORANGE" }, result["-"]);
        }

        [Fact]
        public void ParseInput_RemovesSurroundingQuotes()
        {
            // Arrange
            var parser = new InputParser();
            string input = "+\"cat\" -\"dog\" \"something\"";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";
            var notations = new List<string> { "+", "-" };

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            Assert.Equal(new List<string> { "CAT" }, result["+"]);
            Assert.Equal(new List<string> { "DOG" }, result["-"]);
            Assert.Equal(new List<string> { "SOMETHING" }, result[""]);
        }

        [Fact]
        public void ParseInput_ReturnsEmptyLists_WhenNoMatches()
        {
            // Arrange
            var parser = new InputParser();
            string input = "12345 67890";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";  // Won't match numbers
            var notations = new List<string> { "+", "-" };

            // Act
            var result = parser.ParseInput(input, pattern, notations);

            // Assert
            Assert.Empty(result["-"]);
            Assert.Empty(result["+"]);
        }
    }
}
