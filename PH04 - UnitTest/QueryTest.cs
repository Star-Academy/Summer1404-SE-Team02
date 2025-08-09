using System.Collections.Generic;
using FluentAssertions;
using Xunit;
// using InvertedIndexIR.InputParser;
using ParseInput;
using Moq;

namespace QueryTests
{
    public class QueryTests
    {
        [Fact]
        public void GetWordsOfType_ReturnsCorrectWords_ForExistingKey()
        {
            // Arrange
            var parsedWords = new Dictionary<string, List<string>>
            {
                { "+", new List<string> { "cat", "dog" } },
                { "-", new List<string> { "mouse", "rat" } }
            };
            var query = new Query(parsedWords);

            // Act
            var result = query.GetWordsOfType("+");

            // Assert
            result.Should().BeEquivalentTo(new List<string> { "cat", "dog" });
        }

        [Fact]
        public void GetWordsOfType_ReturnsEmptyList_ForNonExistingKey()
        {
            // Arrange
            var parsedWords = new Dictionary<string, List<string>>
            {
                { "+", new List<string> { "cat", "dog" } }
            };
            var query = new Query(parsedWords);

            // Act
            var result = query.GetWordsOfType("-");

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetWordsOfType_ReturnsCorrectWords_ForMultipleKeys()
        {
            // Arrange
            var parsedWords = new Dictionary<string, List<string>>
            {
                { "+", new List<string> { "apple" } },
                { "-", new List<string> { "banana", "orange" } },
                { "", new List<string> { "grape" } }
            };
            var query = new Query(parsedWords);

            // Act
            var andWords = query.GetWordsOfType("");
            var orWords = query.GetWordsOfType("+");
            var notWords = query.GetWordsOfType("-");

            // Assert
            andWords.Should().ContainSingle(x => x == "grape");
            orWords.Should().ContainSingle(x => x == "apple");
            notWords.Should().BeEquivalentTo(new List<string> { "banana", "orange" });
        }
    }
}
