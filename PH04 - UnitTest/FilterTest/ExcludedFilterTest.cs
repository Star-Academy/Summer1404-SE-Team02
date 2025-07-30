using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;


namespace FilterTest
{
    public class ExcludedFilterTests
    {
        [Fact]
        public void ApplyFilter_WithMinusWords_ReturnsNotIncludingMatchingDocs()
        {
            // Arrange
            var mockQuery = new Mock<IQuery>();
            var mockIndexSearch = new Mock<IInvertedIndexSearch>();
            var filter = new ExcludedFilter(mockIndexSearch.Object);
            var index = new InvertedIndex();
            index.documentNames = new HashSet<string> { "doc1", "doc2", "doc3", "doc4" };

            mockQuery.Setup(q => q.GetWordsOfType("-")).Returns(new List<string> { "apple", "banana" });

            mockIndexSearch.Setup(i => i.Search("apple", It.IsAny<InvertedIndex>()))
                .Returns(new List<string> { "doc1", "doc2" });
            mockIndexSearch.Setup(i => i.Search("banana", It.IsAny<InvertedIndex>()))
                .Returns(new List<string> { "doc2", "doc3" });

            // Act
            var result = filter.ApplyFilter(mockQuery.Object, index);

            // Assert
            var expected = new HashSet<string> { "doc4" };
            Assert.Equal(expected, new HashSet<string>(result));
        }
        

        [Fact]
        public void ApplyFilter_WithNoMinusWords_ReturnsAllDocuments()
        {
            // Arrange
            var mockQuery = new Mock<IQuery>();
            var mockIndexSearch = new Mock<IInvertedIndexSearch>();
            var filter = new ExcludedFilter(mockIndexSearch.Object);
            var index = new InvertedIndex();
            index.documentNames = new HashSet<string> { "docA", "docB" };

            mockQuery.Setup(q => q.GetWordsOfType("-")).Returns(new List<string>());
            
            // Act
            var result = filter.ApplyFilter(mockQuery.Object, index);

            // Assert
            Assert.Equal(new List<string> { "docA", "docB" }, result);
        }
        
    }
}
