using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;


namespace NormalizerTests
{
  public class NormalizerTest
  {
      private readonly INormalizer _sut;

      public NormalizerTest()
      {
          _sut = new BasicNormalizer();
      }
      
        [Theory]
        [InlineData("Th!s!s A Wr#ong Test.", "THSS A WRONG TEST")]
        [InlineData(" ", " ")]
        [InlineData("", "")]
        [InlineData(".", "")]
        [InlineData("Th!!s A Wr# #ong Test.", "THS A WR ONG TEST")]
    public void ValidateNormalizer(string input, string expectedResult)
        {
            //Arrange

            //Act
            string normalized = _sut.Normalize(input);

            //Assert
            normalized.Should().Be(expectedResult);
        }
  }
}
