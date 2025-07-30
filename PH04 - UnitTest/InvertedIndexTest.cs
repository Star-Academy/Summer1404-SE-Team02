using System;
using Xunit;
using Moq;

namespace InvertedIndexTests
{
  public class InvertedIndexTests
  {
    [Theory]
    [InlineData("friend is in this text", new[] { "FRIEND", "IS", "IN", "THIS", "TEXT" }, "friend")]
    [InlineData("friend is in this text", new[] { "FRIEND", "IS", "IN", "THIS", "TEXT" }, "Friend")]
    [InlineData("friend friend is in this text", new[] { "FRIEND", "FRIEND", "IS", "IN", "THIS", "TEXT" }, "friend")]

    public void AddDocument_AddsWords_ToIndex(string content, string[] tokenized, string word)
    {
      // Arrange
      var mockTokenizer = new Mock<ITokenizer>();
      var mockNormalizer = new Mock<INormalizer>();

      mockNormalizer.Setup(n => n.Normalize(It.IsAny<string>()))
                    .Returns<string>(s => s.ToUpper());

      mockTokenizer.Setup(t => t.Tokenize(It.IsAny<string>()))
                   .Returns(tokenized);

      var index = new InvertedIndex(mockTokenizer.Object, mockNormalizer.Object);

      // Act
      index.AddDocument(content, "doc.txt");

      // Assert
      var results = index.Search(word.ToUpper());
      Assert.Contains("doc.txt", results);
      Assert.Single(results);
    }
   
    [Fact]
    public void Search_ReturnsEmpty_WhenWordNotFound()
    {
        var index = new InvertedIndex();
        index.AddDocument("Some text", "doc1.txt");

        var results = index.Search("missing");

        Assert.Empty(results);
    }

    [Fact]
    public void GetDocumentNames_Returns_All_AddedAddresses()
    {
        var index = new InvertedIndex();
        index.AddDocument("text one", "a.txt");
        index.AddDocument("text two", "b.txt");

        var docs = index.GetDocumentNames();

        Assert.Contains("a.txt", docs);
        Assert.Contains("b.txt", docs);
        Assert.Equal(2, docs.Count());
    }

  }

}
