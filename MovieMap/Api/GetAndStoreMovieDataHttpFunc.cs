using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieMap.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieMap
{
    public class GetAndStoreMovieDataHttpFunc
    {
        private readonly ITvMazeApiService _tvMazeService;

        public GetAndStoreMovieDataHttpFunc(ITvMazeApiService tvMazeService)
        {
            _tvMazeService = tvMazeService;
        }

        [FunctionName(nameof(GetAndStoreMovieDataHttpFunc))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetAndStoreMovieData trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            try
            {
                var model = JsonSerializer.Deserialize<GetAndStoreMovieDataModel>(requestBody);
                if (model == null || string.IsNullOrEmpty(model.MovieName))
                {
                    log.LogError("Invalid request");
                    return new BadRequestResult();
                }
                await _tvMazeService.GetMovie(model.MovieName);
            }
            catch(Exception e)
            {
                log.LogError("Could not parse request body", e);
                return new BadRequestResult();
            }

            return new OkObjectResult("Ok");
        }
    }
}
