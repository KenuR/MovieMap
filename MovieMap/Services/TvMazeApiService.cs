using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieMap.Config;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieMap.Services
{
    public class TvMazeApiService : ITvMazeApiService
    {
        private readonly ILogger<TvMazeApiService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDatabaseService _databaseService;
        private readonly TvMazeApiOptions _apiOptions;

        public TvMazeApiService(
            IHttpClientFactory httpClientFactory,
            ILogger<TvMazeApiService> logger,
            IDatabaseService databaseService,
            IOptions<TvMazeApiOptions> apiOptions
            )
        {
            _apiOptions = apiOptions.Value;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _databaseService = databaseService;
        }

        public async Task GetMovie(string movieName)
        {
            _logger.LogInformation("Getting {movieName} information from TvMaze.", movieName);

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri(_apiOptions.ApiUrl);

            var requestUri = new Uri($"singlesearch/shows?q={Uri.EscapeDataString(movieName.ToLower())}", UriKind.Relative);
            var res = await httpClient.GetAsync(requestUri);
            res.EnsureSuccessStatusCode();

            using var stream = await res.Content.ReadAsStreamAsync();

            var response = JsonSerializer.Deserialize<SingleSearchModel>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            _databaseService.StoreMovie(response);
        }
    }
    public interface ITvMazeApiService
    {
        Task GetMovie(string movieName);
    }
}
