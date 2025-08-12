using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using InvertedIndexIR.InvertedIndexDocumentAdder;
using InvertedIndexIR.InvertedIndexDocumentAdder.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using Xunit;
using NSubstitute;


namespace InvertedIndexTests
{
  public class InvertedIndexAddDocumentTests
  {
    private readonly ITokenizer _tokenizer;
    private readonly INormalizer _normalizer;
    private readonly IInvertedIndexDocumentAdder _sut;
    public InvertedIndexAddDocumentTests()
    {
      _tokenizer = NSubstitute.Substitute.For<ITokenizer>();
      _normalizer = NSubstitute.Substitute.For<INormalizer>();
      _sut = new InvertedIndexDocumentAdder(_tokenizer, _normalizer);
    }
    [Theory]
    [InlineData("friend is in this text", new[] { "FRIEND", "IS", "IN", "THIS", "TEXT" }, "friend")]
    [InlineData("friend is in this text", new[] { "FRIEND", "IS", "IN", "THIS", "TEXT" }, "Friend")]
    [InlineData("friend friend is in this text", new[] { "FRIEND", "FRIEND", "IS", "IN", "THIS", "TEXT" }, "friend")]
    public void AddDocument_AddsWords_ToIndex(string content, string[] tokenized, string word)
    {
      // Arrange
      
      var index = new InvertedIndex();

      _normalizer.Normalize(Arg.Any<string>())
        .Returns(callInfo => callInfo.Arg<string>().ToUpper());
      _tokenizer.Tokenize(Arg.Any<string>())
        .Returns(tokenized);
      
      // Act
      _sut.AddDocument(content, "doc.txt", index);

      // Assert
      for (int i = 0; i < tokenized.Length; i++)
      {
        var results = index.WordDocMap[tokenized[i]];
        results.Should().Contain(kv => kv.Key == "doc.txt" && kv.Value == i);
      }
    }
   

    [Fact]
    public void GetDocumentNames_Returns_All_AddedAddresses()
    {
        // Arrange
        var index = new InvertedIndex();
        _normalizer.Normalize(Arg.Any<string>())
          .Returns(callInfo => callInfo.Arg<string>().ToUpper());
        _tokenizer.Tokenize(Arg.Any<string>())
          .Returns(new string[]{"word1", "word2"});
        
        //Act
        _sut.AddDocument("text one", "a.txt", index);
        _sut.AddDocument("text two", "b.txt", index);
      
        var docs = index.DocumentNames;
        
        //Assert
        docs.Should().HaveCount(2).And.Contain("a.txt", "b.txt");
    }

  }

}
