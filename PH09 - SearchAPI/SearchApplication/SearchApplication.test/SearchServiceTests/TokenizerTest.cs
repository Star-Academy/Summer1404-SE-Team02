using System;
using FluentAssertions;
using Xunit;

namespace TokenizerTests
{
  public class TokenizerTest
  {
      private readonly ITokenizer _sut;

      public TokenizerTest()
      {
          _sut = new BasicTokenizer();
      }
      
        [Theory]
        [InlineData("THSS A WRONG TEST", new[] {"THSS", "A", "WRONG", "TEST"})]
        [InlineData(" ", new string[0])]
        [InlineData("", new string[0])]
        [InlineData("P\nHAA", new[] {"P", "HAA"})]
        [InlineData("THS A  WR ONG   \n\t  \r\n TEST \n", new[] {"THS", "A", "WR", "ONG", "TEST"})]
    public void ValidateTokenizer(string input, string[] expectedResult)
        {
            //Arrange

            //Act
            string[] tokenized = _sut.Tokenize(input);

            //Assert
            tokenized.Should().BeEquivalentTo(expectedResult);
        }
  }
}
