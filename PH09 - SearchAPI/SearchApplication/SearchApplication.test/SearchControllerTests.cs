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
            Assert.NotNull(result);
            Assert.Equal(2, resultDocuments.Count());
            Assert.Equal(new List<string> { "Doc1", "Doc2" }, resultDocuments);
            Assert.Equal(200, result.StatusCode);
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
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Query is required", result.Value);
        }

    }
}