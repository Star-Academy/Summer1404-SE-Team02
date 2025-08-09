using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using InvertedIndexWebApi.ExtendedSearch;
using InvertedIndexWebApi.Filters;
using InvertedIndexWebApi.InvertedIndexDTO;
using InvertedIndexWebApi.Query;
using Xunit;
using Moq;

namespace ExtendedSearchTests
{
    public class ExtendedSearchTests
    {
        [Fact]
        public void Search_Returns_Intersection_Of_All_Filters()
        {
            // Arrange
            var index = new InvertedIndex();
            index.documentNames = new HashSet<string>() { "doc1", "doc2",  "doc3", "doc4" };
            var query = Mock.Of<IQuery>();

            var mockFilter1 = new Mock<IFilter>();
            mockFilter1.Setup(f => f.ApplyFilter(It.IsAny<IQuery>(), It.IsAny<InvertedIndex>()))
                       .Returns(new HashSet<string> { "doc1", "doc2" });

            var mockFilter2 = new Mock<IFilter>();
            mockFilter2.Setup(f => f.ApplyFilter(It.IsAny<IQuery>(), It.IsAny<InvertedIndex>()))
                       .Returns(new HashSet<string> { "doc2", "doc3" });


            var search = new ExtendedSearch();
            search.AddFilter(mockFilter1.Object);
            search.AddFilter(mockFilter2.Object);

            // Act
            var result = search.Search(query, index).ToHashSet();

            // Assert
            result.Should().ContainSingle(x => x == "doc2");
        }

        [Fact]
        public void Search_WithNoFilters_Returns_AllDocuments()
        {
            // Arrange 
            var index = new InvertedIndex();
            // mockIndex.Setup(i => i.GetDocumentNames()).Returns(new List<string> { "doc1", "doc2" });
            index.documentNames = new HashSet<string>() { "doc1", "doc2" };
            var query = Mock.Of<IQuery>();
            // Act
            var search = new ExtendedSearch();

            var result = search.Search(query, index).ToList();
            // Assert
            result.Should().HaveCount(2).And.Contain(new[] { "doc1", "doc2" });
        }
    }
}
