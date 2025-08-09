using FluentAssertions;
using WebApplication1.Controllers;
using Moq;
using InvertedIndexWebApi.Normalizer;
using Microsoft.AspNetCore.Mvc;

namespace SearchApplication.test
{
    public class SearchControllerTests
    {
        [Fact]
        public void SearchReturnsCorrectResult()
        {
            //arrange
            var searchService = new Mock<ISearchService>();
            searchService.Setup(n => n.Search(It.IsAny<string>()))
                    .Returns(new List<string> { "Doc1", "Doc2"});
            var controller = new SearchController(searchService.Object);

            //act
            var actionResult = controller.Search("folan");

            //assert
            var result = actionResult as OkObjectResult;
            var resultDocuments = result.Value as List<string>;
            result.Should().NotBeNull();
            resultDocuments.Should().HaveCount(2);
            resultDocuments.Should().BeEquivalentTo(new List<string> { "Doc1", "Doc2" });
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public void SearchReturnsBadRequestWhenInputEmpty()
        {
            // Arrange
            var searchService = new Mock<ISearchService>();
            var controller = new SearchController(searchService.Object);

            // Act
            var actionResult = controller.Search("");

            // Assert
            var result = actionResult as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Query is required");
        }

    }
}