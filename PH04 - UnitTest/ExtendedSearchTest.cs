using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ExtendedSearchTests
{
    public class ExtendedSearchTests
    {
        [Fact]
        public void Search_Returns_Intersection_Of_All_Filters()
        {
            // Arrange
            var mockIndex = new Mock<IInvertedIndex>();
            mockIndex.Setup(i => i.GetDocumentNames()).Returns(new List<string> { "doc1", "doc2", "doc3", "doc4" });
            var query = Mock.Of<IQuery>();

            var mockFilter1 = new Mock<IFilter>();
            mockFilter1.Setup(f => f.ApplyFilter(It.IsAny<IQuery>(), It.IsAny<IInvertedIndex>()))
                       .Returns(new HashSet<string> { "doc1", "doc2" });

            var mockFilter2 = new Mock<IFilter>();
            mockFilter2.Setup(f => f.ApplyFilter(It.IsAny<IQuery>(), It.IsAny<IInvertedIndex>()))
                       .Returns(new HashSet<string> { "doc2", "doc3" });


            var search = new ExtendedSearch(mockIndex.Object);
            search.AddFilter(mockFilter1.Object);
            search.AddFilter(mockFilter2.Object);

            // Act
            var result = search.Search(query).ToHashSet();

            // Assert
            Assert.Single(result);
            Assert.Contains("doc2", result);
        }

        [Fact]
        public void Search_WithNoFilters_Returns_AllDocuments()
        {
            // Arrange 
            var mockIndex = new Mock<IInvertedIndex>();
            mockIndex.Setup(i => i.GetDocumentNames()).Returns(new List<string> { "doc1", "doc2" });

            var query = Mock.Of<IQuery>();
            // Act
            var search = new ExtendedSearch(mockIndex.Object);

            var result = search.Search(query).ToList();
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("doc1", result);
            Assert.Contains("doc2", result);
        }
    }
}
