using System;
using Xunit;
using Moq;

namespace InvertedIndexTests
{
  public class InvertedIndexAddDocumentTests
  {
    private Mock<ITokenizer> tokenizer;
    private Mock<INormalizer> normalizer;
    public InvertedIndexAddDocumentTests()
    {
      tokenizer = new Mock<ITokenizer>();
      normalizer = new Mock<INormalizer>();
    }
    [Theory]
    [InlineData("friend is in this text", new[] { "FRIEND", "IS", "IN", "THIS", "TEXT" }, "friend")]
    [InlineData("friend is in this text", new[] { "FRIEND", "IS", "IN", "THIS", "TEXT" }, "Friend")]
    [InlineData("friend friend is in this text", new[] { "FRIEND", "FRIEND", "IS", "IN", "THIS", "TEXT" }, "friend")]

    public void AddDocument_AddsWords_ToIndex(string content, string[] tokenized, string word)
    {
      // Arrange
      
      var index = new InvertedIndex();

      normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                    .Returns<string>(s => s.ToUpper());

      tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                   .Returns(tokenized);

      var indexAdder = new InvertedIndexDocumentAdder(tokenizer.Object, normalizer.Object);

      // Act
      indexAdder.AddDocument(content, "doc.txt", index);

      // Assert
      for (int i = 0; i < tokenized.Length; i++)
      {
        var results = index.invertedIndex[tokenized[i]];
        Assert.Contains(new KeyValuePair<string, int>("doc.txt", i), results);
      }
      // Assert.Equal(tokenized.Length, index.invertedIndex.Count);
    }
   

    [Fact]
    public void GetDocumentNames_Returns_All_AddedAddresses()
    {
        var index = new InvertedIndex();
        var indexAdder = new InvertedIndexDocumentAdder(tokenizer.Object,  normalizer.Object);
        indexAdder.AddDocument("text one", "a.txt", index);
        indexAdder.AddDocument("text two", "b.txt", index);
        normalizer.Setup(n => n.Normalize(It.IsAny<string>()))
          .Returns<string>(s => s.ToUpper());

        tokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
          .Returns(new string[]{"word1", "word2"});
        var docs = index.documentNames;

        Assert.Contains("a.txt", docs);
        Assert.Contains("b.txt", docs);
        Assert.Equal(2, docs.Count());
    }

  }

}
