using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using InvertedIndexIR.InputParser;
using InvertedIndexIR.InputParser.Abstraction;

namespace InvertedIndexTests
{
    public class InputParserTests
    {
        private IInputParser _sut;

        public InputParserTests()
        {
            _sut = new InputParser();
        }
        
        [Fact]
        public void ParseInput_ReturnsCorrectDictionary_ForSimpleInput()
        {
            // Arrange
            string input = "+cat -dog fish";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";

            // Act
            var result = _sut.ParseInput(input, pattern);

            // Assert
            result.Should().BeEquivalentTo(new List<string> {"+CAT", "-DOG", "FISH"});

        }

        [Fact]
        public void ParseInput_PutsUnprefixedWordsInDefaultKey()
        {
            // Arrange
            string input = "apple \"banana is good\" -orange";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";

            // Act
            var result = _sut.ParseInput(input, pattern);

            // Assert
            result.Should().BeEquivalentTo(new List<string> { "APPLE", "BANANA IS GOOD", "-ORANGE" });

        }

        [Fact]
        public void ParseInput_RemovesSurroundingQuotes()
        {
            // Arrange
            string input = "+\"cat\" -\"dog\" \"something\"";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";

            // Act
            var result = _sut.ParseInput(input, pattern);

            // Assert
            result.Should().BeEquivalentTo(new List<string> { "+CAT", "-DOG", "SOMETHING" });

        }

        [Fact]
        public void ParseInput_ReturnsEmptyLists_WhenNoMatches()
        {
            // Arrange
            string input = "";
            string pattern = @"([+-]?""[^""]+""|[+-]?\S+)";

            // Act
            var result = _sut.ParseInput(input, pattern);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
