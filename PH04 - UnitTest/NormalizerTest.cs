using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Moq;


namespace NormalizerTests
{
  public class NormalizerTest
  {
        [Theory]
        [InlineData("Th!s!s A Wr#ong Test.", "THSS A WRONG TEST")]
        [InlineData(" ", " ")]
        [InlineData("", "")]
        [InlineData(".", "")]
        [InlineData("Th!!s A Wr# #ong Test.", "THS A WR ONG TEST")]
    public void ValidateNormalizer(string input, string expectedResult)
        {
            //Arrange
            var normalizer = new BasicNormalizer();

            //Act
            string normalized = normalizer.Normalize(input);

            //Assert
            normalized.Should().Be(expectedResult);
        }
  }
}
