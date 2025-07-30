
using System;
using Xunit;

namespace QueryTests
{
  public class QueryTest
  {
    public static IEnumerable<object[]> ParseInputTestData1 =>
          new List<object[]>
          {
              new object[]
              {
                  "get sick home +mine +yours +none -fine",
                  new[] { "+", "-", "" },
                  new List<List<string>>
                  {
                      new List<string> { "MINE", "YOURS", "NONE" },
                      new List<string> { "FINE" },
                      new List<string> { "GET", "SICK", "HOME"}
                  }
              }
          };

    public static IEnumerable<object[]> ParseInputTestData2 =>
          new List<object[]>
          {
              new object[]
              {
                  "get sick home +mine +yours +none + -fine",
                  new[] { "+", "-", "" },
                  new List<List<string>>
                  {
                      new List<string> { "MINE", "YOURS", "NONE" },
                      new List<string> { "FINE" },
                      new List<string> { "GET", "SICK", "HOME"}
                  }
              }
          };
    public static IEnumerable<object[]> ParseInputTestData4 =>
          new List<object[]>
          {
              new object[]
              {
                  "get sick home +mine +yours +none +-fine",
                  new[] { "+", "-", "" },
                  new List<List<string>>
                  {
                      new List<string> { "MINE", "YOURS", "NONE" },
                      new List<string> { },
                      new List<string> { "GET", "SICK", "HOME"}
                  }
              }
          };
    public static IEnumerable<object[]> ParseInputTestData3 =>
          new List<object[]>
          {
              new object[]
              {
                  "get sick home +mine +yours +none ",
                  new[] { "+", "-", "" },
                  new List<List<string>>
                  {
                      new List<string> { "MINE", "YOURS", "NONE" },
                      new List<string> {  },
                      new List<string> { "GET", "SICK", "HOME"}
                  }
              }
          };
    public static IEnumerable<object[]> ParseInputTestData5 =>
          new List<object[]>
          {
              new object[]
              {
                  "get sick home +mine +yours +none ",
                  new[] { "+" },
                  new List<List<string>>
                  {
                      new List<string> { "MINE", "YOURS", "NONE" }
                  }
              }
          };
    public static IEnumerable<object[]> ParseInputTestData6 =>
          new List<object[]>
          {
              new object[]
              {
                  "",
                  new[] { "+", "-", "" },
                  new List<List<string>>
                  {
                      new List<string> { },
                      new List<string> { },
                      new List<string> { }
                  }
              }
          };
    public static IEnumerable<object[]> ParseInputTestData7 =>
          new List<object[]>
          {
              new object[]
              {
                  "get sick home +mine +yours +none -fine",
                  new string[0],
                  new List<List<string>>{}
              }
          };
    [Theory]
    [MemberData(nameof(ParseInputTestData1))]
    //[MemberData(nameof(ParseInputTestData2))]
    [MemberData(nameof(ParseInputTestData3))]
    //[MemberData(nameof(ParseInputTestData4))]
    [MemberData(nameof(ParseInputTestData5))]
    [MemberData(nameof(ParseInputTestData6))]
    [MemberData(nameof(ParseInputTestData7))]
    public void ValidateParseInput(string input, string[] notations, List<List<string>> expectedResult)
    {
      //Arrange
      var query = new Query();

      //Act
      var parseResult = new List<List<string>> { };
      query.ParseInput(input);
      foreach (string notation in notations)
      {
        parseResult.Add(query.getWordsOfType(notation));
      }

      //Assert
      Assert.Equal(expectedResult, parseResult);
    }
  }
}
