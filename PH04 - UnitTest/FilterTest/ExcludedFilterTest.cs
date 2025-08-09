using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
            result.Should().ContainSingle().And.Contain(new HashSet<string> { "doc4" });
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
            result.Should().HaveCount(2).And.Contain(new HashSet<string> { "docA", "docB" });
        }
        
    }
}
