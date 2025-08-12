using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;
using FluentAssertions;
using InvertedIndexIR.InvertedIndexSearch;
using NSubstitute;
using Xunit;

namespace InvertedIndexTests
{
    public class InvertedIndexSearchTests
    {
        private readonly InvertedIndex _invertedIndex;
        private readonly ITokenizer _tokenizer;
        private readonly INormalizer _normalizer;
        private readonly InvertedIndexSearch _sut;
        public InvertedIndexSearchTests()
        {
            _tokenizer = NSubstitute.Substitute.For<ITokenizer>();
            _normalizer = NSubstitute.Substitute.For<INormalizer>();
            _invertedIndex = new InvertedIndex();
            AddToIndex("FRIEND", "doc1.txt", 0);
            AddToIndex("INVERTED", "doc1.txt", 1);
            AddToIndex("INDEX", "doc1.txt", 2);
            AddToIndex("SEARCH", "doc1.txt", 3);
            AddToIndex("FRIEND", "doc2.txt", 1);
            _sut = new InvertedIndexSearch(_tokenizer, _normalizer);
        }

        private void AddToIndex(string word, string doc, int pos)
        {
            if (!_invertedIndex.WordDocMap.ContainsKey(word))
                _invertedIndex.WordDocMap[word] = new List<KeyValuePair<string, int>>();
            _invertedIndex.WordDocMap[word].Add(new KeyValuePair<string, int>(doc, pos));
        }

        [Fact]
        public void Search_ReturnsCorrectDocuments_WhenWordExists()
        {
            //Arrange
            _normalizer.Normalize(Arg.Any<string>())
                .Returns(callInfo => callInfo.Arg<string>().ToUpper());
            _tokenizer.Tokenize(Arg.Any<string>())
                .Returns(new[] { "FRIEND" });
            
            //Act
            var result = _sut.Search("FRIEND", _invertedIndex);
            
            //Assert
            result.Should().HaveCount(2)
                .And.Contain(new[] { "doc1.txt", "doc2.txt" });
        }

        [Fact]
        public void Search_ReturnsCorrectDocuments_WhenPhraseExists()
        {
            //Arrange
            _normalizer.Normalize(Arg.Any<string>())
                .Returns(callInfo => callInfo.Arg<string>().ToUpper());
            _tokenizer.Tokenize(Arg.Any<string>())
                .Returns(new string[] {"INVERTED", "INDEX", "SEARCH"});
            
            //Act
            var result = _sut.Search("INVERTED INDEX SEARCH", _invertedIndex);
            
            //Assert
            result.Should().ContainSingle(x => x == "doc1.txt");
        }

        [Fact]
        public void Search_ReturnsEmpty_WhenWordNotFound()
        {
            //Arrange
            _normalizer.Normalize(Arg.Any<string>())
                .Returns(callInfo => callInfo.Arg<string>().ToUpper());
            _tokenizer.Tokenize(Arg.Any<string>())
                .Returns(new string[] {"MISSING"});
            //Act
            var result = _sut.Search("MISSING", _invertedIndex);
            //Assert
            result.Should().BeEmpty();
        }
    
        [Fact]
        public void Search_ReturnsEmpty_WhenPhraseWordsExistButNotTogether()
        {
            //Arrange
            _normalizer.Normalize(Arg.Any<string>())
                .Returns(callInfo => callInfo.Arg<string>().ToUpper());
            _tokenizer.Tokenize(Arg.Any<string>())
                .Returns(new string[] {"INVERTED", "INDEX", "SEARCH"});
            
            AddToIndex("INVERTED", "doc2.txt", 0);
            AddToIndex("INDEX", "doc2.txt", 2);
            AddToIndex("SEARCH", "doc2.txt", 5);
            
            //Act
            var result = _sut.Search("INVERTED INDEX SEARCH", _invertedIndex);
            //Assert
            result.Should().ContainSingle(x => x == "doc1.txt");
        }

        [Fact]
        public void Search_ReturnsMultipleDocuments_WhenPhraseExistsInMultipleFiles()
        {
            //Arrange
            _normalizer.Normalize(Arg.Any<string>())
                .Returns(callInfo => callInfo.Arg<string>().ToUpper());
            _tokenizer.Tokenize(Arg.Any<string>())
                .Returns(new string[] {"INVERTED", "INDEX", "SEARCH"});
           
            AddToIndex("INVERTED", "doc3.txt", 0);
            AddToIndex("INDEX", "doc3.txt", 1);
            AddToIndex("SEARCH", "doc3.txt", 2);
            
            //Act
            var result = _sut.Search("INVERTED INDEX SEARCH", _invertedIndex);
            
            //Assert
            result.Should().HaveCount(2)
                .And.Contain(new[] { "doc1.txt", "doc3.txt" });
        }

        [Fact]
        public void Search_ReturnsEmpty_WhenPhraseDoesNotExistAnywhere()
        {
            //Arrange
            _normalizer.Normalize(Arg.Any<string>())
                .Returns(callInfo => callInfo.Arg<string>().ToUpper());
            _tokenizer.Tokenize(Arg.Any<string>())
                .Returns(new string[] {"NOTFOUND", "PHRASE"});
            
            //Act
            var result = _sut.Search("NOTFOUND PHRASE", _invertedIndex);
            
            //Assert
            result.Should().BeEmpty();
        }
  }

}
