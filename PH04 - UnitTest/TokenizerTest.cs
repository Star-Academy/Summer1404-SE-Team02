
using System;
using FluentAssertions;
using Xunit;
using Moq;


namespace TokenizerTests
{
  public class TokenizerTest
  {
        [Theory]
        [InlineData("THSS A WRONG TEST", new[] {"THSS", "A", "WRONG", "TEST"})]
        [InlineData(" ", new string[0])]
        [InlineData("", new string[0])]
        [InlineData("P\nHAA", new[] {"P", "HAA"})]
        [InlineData("THS A  WR ONG   \n\t  \r\n TEST \n", new[] {"THS", "A", "WR", "ONG", "TEST"})]
    public void ValidateTokenizer(string input, string[] expectedResult)
        {
            //Arrange
            var tokenizer = new BasicTokenizer();

            //Act
            string[] tokenized = tokenizer.Tokenize(input);

            //Assert
            tokenized.Should().BeEquivalentTo(expectedResult);
        }
  }
}
