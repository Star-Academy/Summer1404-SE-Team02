using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApp;
using SearchApp.Abstraction;
using SearchApplication.ActivityResources;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private ActivitySource activitySource;

        public SearchController(ISearchService searchService, Instrumentation instrumentation)
        {
            _searchService = searchService;
            activitySource = instrumentation.ActivitySource;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string query)
        {
            
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query is required");

            using var activity = activitySource.StartActivity("SearchController.Search");
            activity?.SetTag("query.raw", query);
            activity?.SetTag("query.length", query.Length);

            var stopwatch = Stopwatch.StartNew();
            var results = _searchService.Search(query);
            stopwatch.Stop();

            activity?.SetTag("results.count", results.Count());
            activity?.SetTag("search.duration.ms", stopwatch.ElapsedMilliseconds);

            return Ok(results);
        }
    }
}
