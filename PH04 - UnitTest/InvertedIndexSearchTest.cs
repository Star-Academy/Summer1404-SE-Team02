using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;
using FluentAssertions;
using Xunit;
using Moq;

namespace InvertedIndexTests
{
    public class InvertedIndexSearchTests
    {
        private InvertedIndex invertedIndex;
        private Mock<ITokenizer> tokenizer;
        private Mock<INormalizer> normalizer;
        public InvertedIndexSearchTests()
        {
            invertedIndex = new InvertedIndex();
            tokenizer = new Mock<ITokenizer>();
            normalizer = new Mock<INormalizer>();
            AddToIndex("FRIEND", "doc1.txt", 0);
            AddToIndex("INVERTED", "doc1.txt", 1);
            AddToIndex("INDEX", "doc1.txt", 2);
            AddToIndex("SEARCH", "doc1.txt", 3);
            AddToIndex("FRIEND", "doc2.txt", 1);
        }

        private void AddToIndex(string word, string doc, int pos)
        {
            if (!invertedIndex.invertedIndex.ContainsKey(word))
                invertedIndex.invertedIndex[word] = new List<KeyValuePair<string, int>>();
            invertedIndex.invertedIndex[word].Add(new KeyValuePair<string, int>(doc, pos));
        }

        [Fact]
        public void Search_ReturnsCorrectDocuments_WhenWordExists()
        {
            normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                .Returns<string>(s => s.ToUpper());

            tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                .Returns(new string[] {"FRIEND"});
            var invertedIndexSearch = new InvertedIndexSearch(tokenizer.Object, normalizer.Object);
            var result = invertedIndexSearch.Search("FRIEND", invertedIndex);
            result.Should().HaveCount(2)
                .And.Contain(new[] { "doc1.txt", "doc2.txt" });
        }

        [Fact]
        public void Search_ReturnsCorrectDocuments_WhenPhraseExists()
        {
            normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                .Returns<string>(s => s.ToUpper());

            tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                .Returns(new string[] {"INVERTED", "INDEX", "SEARCH"});
            var invertedIndexSearch = new InvertedIndexSearch(tokenizer.Object, normalizer.Object);
            var result = invertedIndexSearch.Search("INVERTED INDEX SEARCH", invertedIndex);
            result.Should().ContainSingle(x => x == "doc1.txt");
        }

        [Fact]
        public void Search_ReturnsEmpty_WhenWordNotFound()
        {
            normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                .Returns<string>(s => s.ToUpper());

            tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                .Returns(new string[] {"MISSING"});
            var invertedIndexSearch = new InvertedIndexSearch(tokenizer.Object, normalizer.Object);
            var result = invertedIndexSearch.Search("MISSING", invertedIndex);
            result.Should().BeEmpty();
        }
    
        [Fact]
        public void Search_ReturnsEmpty_WhenPhraseWordsExistButNotTogether()
        {
            normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                .Returns<string>(s => s.ToUpper());

            tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                .Returns(new string[] {"INVERTED", "INDEX", "SEARCH"});
            var invertedIndexSearch = new InvertedIndexSearch(tokenizer.Object, normalizer.Object);
            AddToIndex("INVERTED", "doc2.txt", 0);
            AddToIndex("INDEX", "doc2.txt", 2);
            AddToIndex("SEARCH", "doc2.txt", 5);
            var result = invertedIndexSearch.Search("INVERTED INDEX SEARCH", invertedIndex);
            result.Should().ContainSingle(x => x == "doc1.txt");
        }

        [Fact]
        public void Search_ReturnsMultipleDocuments_WhenPhraseExistsInMultipleFiles()
        {
            normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                .Returns<string>(s => s.ToUpper());

            tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                .Returns(new string[] {"INVERTED", "INDEX", "SEARCH"});
            var invertedIndexSearch = new InvertedIndexSearch(tokenizer.Object, normalizer.Object);
            AddToIndex("INVERTED", "doc3.txt", 0);
            AddToIndex("INDEX", "doc3.txt", 1);
            AddToIndex("SEARCH", "doc3.txt", 2);
            var result = invertedIndexSearch.Search("INVERTED INDEX SEARCH", invertedIndex);
            
            result.Should().HaveCount(2)
                .And.Contain(new[] { "doc1.txt", "doc3.txt" });
        }

        [Fact]
        public void Search_ReturnsEmpty_WhenPhraseDoesNotExistAnywhere()
        {
            normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                .Returns<string>(s => s.ToUpper());

            tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                .Returns(new string[] {"NOTFOUND", "PHRASE"});
            var invertedIndexSearch = new InvertedIndexSearch(tokenizer.Object, normalizer.Object);
            var result = invertedIndexSearch.Search("NOTFOUND PHRASE", invertedIndex);
            result.Should().BeEmpty();
        }
  }

}
