using System.Collections.Generic;
using Xunit;
// using InvertedIndexIR.InputParser;
using ParseInput;

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
            Assert.Equal(new List<string> { "cat", "dog" }, result);
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
            Assert.Empty(result);
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
            var andWords = query.GetWordsOfType("+");
            var orWords = query.GetWordsOfType("-");
            var notWords = query.GetWordsOfType("");

            // Assert
            Assert.Single(andWords);
            Assert.Equal(2, orWords.Count);
            Assert.Contains("grape", notWords);
        }
    }
}
