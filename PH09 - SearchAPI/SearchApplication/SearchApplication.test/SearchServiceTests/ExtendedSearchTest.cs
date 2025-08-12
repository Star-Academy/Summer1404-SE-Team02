using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Search.Abstraction;
using InvertedIndexIR.Search.Extended;
using Xunit;
using NSubstitute;

namespace ExtendedSearchTests
{
    public class ExtendedSearchTests
    {
        private readonly IExtendedSearch _sut;
        private readonly IExtendedSearch _sut2;
        private readonly IFilter _filter1;
        private readonly IFilter _filter2;

        public ExtendedSearchTests()
        {
            _filter1 = NSubstitute.Substitute.For<IFilter>();
            _filter2 = NSubstitute.Substitute.For<IFilter>();
            _sut = new ExtendedSearch(new List<IFilter>{_filter1, _filter2});
            _sut2 = new ExtendedSearch(new List<IFilter>());
        }
        
        [Fact]
        public void Search_Returns_Intersection_Of_All_Filters()
        {
            // Arrange
            var index = new InvertedIndex();
            var query = new Query();
            index.DocumentNames = new HashSet<string>() { "doc1", "doc2",  "doc3", "doc4" };
            
            _filter1.ApplyFilter(Arg.Any<Query>(), Arg.Any<InvertedIndex>())
                .Returns(new List<string> { "doc1", "doc2" });

            _filter2.ApplyFilter(Arg.Any<Query>(), Arg.Any<InvertedIndex>())
                .Returns(new List<string> { "doc2", "doc3" });
            
            // Act
            var result = _sut.Search(query, index);

            // Assert
            result.Should().ContainSingle(x => x == "doc2");
        }

        [Fact]
        public void Search_WithNoFilters_Returns_AllDocuments()
        {
            // Arrange 
            var index = new InvertedIndex();
            var query = new Query();
            index.DocumentNames = new HashSet<string>() { "doc1", "doc2" };
            // Act
            var result = _sut2.Search(query, index).ToList();
            // Assert
            result.Should().HaveCount(2).And.Contain(new[] { "doc1", "doc2" });
        }
    }
}
