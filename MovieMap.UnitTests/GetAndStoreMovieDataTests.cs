using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieMap.Services;
using NSubstitute;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieMap.UnitTests
{
    [TestClass]
    public class GetAndStoreMovieDataTests
    {
        private readonly IHttpClientFactory _client;
        private readonly ILogger<TvMazeApiService> _logger;
        public GetAndStoreMovieDataTests(
            IHttpClientFactory httpClientFactory,
            ILogger<TvMazeApiService> logger)
        {
            _client = httpClientFactory;
            _logger = logger;
        }

        [TestMethod]
        public async Task TestMethod1Async()
        {
            //var httpFactoryMock = Substitute.For<IHttpClientFactory>();
            //httpFactoryMock.CreateClient().Returns

            //var sut = new TvMazeApiService(
            //    Substitute.For<IHttpClientFactory>(),
            //    Substitute.For<ILogger<TvMazeApiService>>(),
            //    Substitute.For<IDatabaseService>()
            //    );

            //sut.GetMovie("");

            //var request = new HttpRequestMessage();
            //var requestContent = new GetAndStoreMovieDataModel("");

            //request.Content = new StringContent(JsonSerializer.Serialize(requestContent));
            //await sut.Run(request, Substitute.For<ILogger>());
        }
    }
}