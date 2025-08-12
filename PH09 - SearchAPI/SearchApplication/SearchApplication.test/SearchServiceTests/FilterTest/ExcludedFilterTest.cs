using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;
using NSubstitute;
using Xunit;

namespace FilterTest
{
    public class ExcludedFilterTests
    {
        private readonly IInvertedIndexSearch _indexSearch;
        private readonly IQueryWordsOfTypeGetter _queryWordsOfTypeGetter;
        private readonly IFilter _sut;

        public ExcludedFilterTests()
        {
            _indexSearch = NSubstitute.Substitute.For<IInvertedIndexSearch>();
            _queryWordsOfTypeGetter = NSubstitute.Substitute.For<IQueryWordsOfTypeGetter>();
            _sut = new ExcludedFilter(_indexSearch, _queryWordsOfTypeGetter);
        }
        [Fact]
        public void ApplyFilter_WithMinusWords_ReturnsNotIncludingMatchingDocs()
        {
            // Arrange
            var query = new Query();
            var index = new InvertedIndex();
            index.DocumentNames = new HashSet<string> { "doc1", "doc2", "doc3", "doc4" };
            
            _queryWordsOfTypeGetter.GetWordsOfType(query, "-")
                .Returns(new List<string>{"apple", "banana"});
            
            _indexSearch.Search("apple", Arg.Any<InvertedIndex>())
                .Returns(new List<string> { "doc1", "doc2" });

            _indexSearch.Search("banana", Arg.Any<InvertedIndex>())
                .Returns(new List<string> { "doc2", "doc3" });

            // Act
            var result = _sut.ApplyFilter(query, index);

            // Assert
            result.Should().ContainSingle().And.Contain(new HashSet<string> { "doc4" });
        }
        

        [Fact]
        public void ApplyFilter_WithNoMinusWords_ReturnsAllDocuments()
        {
            // Arrange
            var query = new Query();
            var index = new InvertedIndex();
            index.DocumentNames = new HashSet<string> { "docA", "docB" };

            _queryWordsOfTypeGetter.GetWordsOfType(query, "-")
                .Returns(new List<string>());
            
            // Act
            var result = _sut.ApplyFilter(query, index);

            // Assert
            result.Should().HaveCount(2).And.Contain(new HashSet<string> { "docA", "docB" });
        }
        
    }
}
