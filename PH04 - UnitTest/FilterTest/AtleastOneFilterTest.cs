using Xunit;
using Moq;
using System.Collections.Generic;

namespace FilterTest
{
    public class AtLeastOneFilterTests
    {
        [Fact]
        public void ApplyFilter_WithPlusWords_ReturnsUnionOfMatchingDocs()
        {
            // Arrange
            var mockQuery = new Mock<IQuery>();
            var mockIndex = new Mock<IInvertedIndex>();
            var filter = new AtLeastOneFilter();

            mockQuery.Setup(q => q.getWordsOfType("+")).Returns(new List<string> { "apple", "banana" });

            mockIndex.Setup(i => i.Search("apple")).Returns(new List<string> { "doc1", "doc2" });
            mockIndex.Setup(i => i.Search("banana")).Returns(new List<string> { "doc2", "doc3" });

            // Act
            var result = filter.ApplyFilter(mockQuery.Object, mockIndex.Object);

            // Assert
            var expected = new HashSet<string> { "doc1", "doc2", "doc3" };
            Assert.Equal(expected, new HashSet<string>(result));
        }

        [Fact]
        public void ApplyFilter_WithNoPlusWords_ReturnsAllDocuments()
        {
            // Arrange
            var mockQuery = new Mock<IQuery>();
            var mockIndex = new Mock<IInvertedIndex>();
            var filter = new AtLeastOneFilter();

            mockQuery.Setup(q => q.getWordsOfType("+")).Returns(new List<string>());

            mockIndex.Setup(i => i.GetDocumentNames()).Returns(new List<string> { "docA", "docB" });

            // Act
            var result = filter.ApplyFilter(mockQuery.Object, mockIndex.Object);

            // Assert
            Assert.Equal(new List<string> { "docA", "docB" }, result);
        }
    }
}
