using System;
using Xunit;
using Moq;

namespace InvertedIndexTests
{
    public class InvertedIndexSearchTests
    {
        private InvertedIndex invertedIndex;
        public InvertedIndexSearchTests()
        {
            invertedIndex = new InvertedIndex(new BasicTokenizer(), new BasicNormalizer());
            AddToIndex("FRIEND", "doc1.txt", 0);
            AddToIndex("INVERTED", "doc1.txt", 1);
            AddToIndex("INDEX", "doc1.txt", 2);
            AddToIndex("SEARCH", "doc1.txt", 3);
            AddToIndex("FRIEND", "doc2.txt", 1);
        }

        private void AddToIndex(string word, string doc, int pos)
        {
            if (!invertedIndex.invertedIndex.ContainsKey(word))
                invertedIndex.invertedIndex[word] = new LinkedList<KeyValuePair<string, int>>();
            invertedIndex.invertedIndex[word].AddLast(new KeyValuePair<string, int>(doc, pos));
        }

        [Fact]
        public void Search_ReturnsCorrectDocuments_WhenWordExists()
        {
            var result = invertedIndex.Search("FRIEND");
            Assert.Equal(2, result.Count());
            Assert.Equal("doc1.txt", result.ElementAt(0));
            Assert.Equal("doc2.txt", result.ElementAt(1));
        }

        [Fact]
        public void Search_ReturnsCorrectDocuments_WhenPhraseExists()
        {
            var result = invertedIndex.Search("INVERTED INDEX SEARCH");
            Assert.Single(result);
            Assert.Equal("doc1.txt", result.ElementAt(0));
        }

        [Fact]
        public void Search_ReturnsEmpty_WhenWordNotFound()
        {
            var result = invertedIndex.Search("MISSING");
            Assert.Empty(result);
        }
    
        [Fact]
        public void Search_ReturnsEmpty_WhenPhraseWordsExistButNotTogether()
        {
            AddToIndex("INVERTED", "doc2.txt", 0);
            AddToIndex("INDEX", "doc2.txt", 2);
            AddToIndex("SEARCH", "doc2.txt", 5);
            var result = invertedIndex.Search("INVERTED INDEX SEARCH");
            Assert.Single(result);
            Assert.Equal("doc1.txt", result.ElementAt(0));
        }

        [Fact]
        public void Search_ReturnsMultipleDocuments_WhenPhraseExistsInMultipleFiles()
        {
            AddToIndex("INVERTED", "doc3.txt", 0);
            AddToIndex("INDEX", "doc3.txt", 1);
            AddToIndex("SEARCH", "doc3.txt", 2);
            var result = invertedIndex.Search("INVERTED INDEX SEARCH");
            Assert.Equal(2, result.Count());
            Assert.Contains("doc1.txt", result);
            Assert.Contains("doc3.txt", result);
        }

        [Fact]
        public void Search_ReturnsEmpty_WhenPhraseDoesNotExistAnywhere()
        {
            var result = invertedIndex.Search("NOTFOUND PHRASE");
            Assert.Empty(result);
        }

  }

}
