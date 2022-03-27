using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieMap.Services;

namespace MovieMap
{
    public class GetHighestRatedPerGenreHttpFunc
    {
        private readonly IGetHighestRatedService _getHighestRatedService;

        public GetHighestRatedPerGenreHttpFunc(IGetHighestRatedService getHighestRatedService)
        {
            _getHighestRatedService = getHighestRatedService;
        }

        [FunctionName(nameof(GetHighestRatedPerGenreHttpFunc))]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "genres/{genre:alpha:length(1,40)}")] HttpRequest req,
            string genre,
            ILogger log)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return new NotFoundResult();
            }

            var movie = _getHighestRatedService.GetHighestRatedMoviePerGenre(genre);
            if (movie == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(movie);
        }
    }
}
