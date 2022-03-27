using Microsoft.Extensions.Logging;

namespace MovieMap.Services
{
    public interface IGetHighestRatedService
    {
        MovieModel GetHighestRatedMoviePerGenre(string genre);
    }

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
            return _databaseService.GetHighestRatedPerGenre(genre);
        }
    }
}
