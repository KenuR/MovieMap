using Microsoft.Extensions.Logging;

namespace MovieMap.Services
{
    public class GetHighestRatedService : IGetHighestRatedService
    {
        private readonly ILogger<TvMazeApiService> _logger;
        private readonly IDatabaseService _databaseService;

        public GetHighestRatedService(
            ILogger<TvMazeApiService> logger,
            IDatabaseService databaseService
            )
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public MovieModel GetHighestRatedMoviePerGenre(string genre)
        {
            _logger.LogInformation("Getting highest rated movie within genre: '{genre}'");
            return _databaseService.GetHighestRatedPerGenre(genre);
        }
    }
    public interface IGetHighestRatedService
    {
        MovieModel GetHighestRatedMoviePerGenre(string genre);
    }
}
