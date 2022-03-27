using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieMap.Config;
using MovieMap.Services;

[assembly: FunctionsStartup(typeof(MovieMap.Startup))]

namespace MovieMap
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();

            builder.Services.AddTransient<ITvMazeApiService, TvMazeApiService>();
            builder.Services.AddTransient<IGetHighestRatedService, GetHighestRatedService>();
            builder.Services.AddTransient<IDatabaseService, DatabaseService>();

            builder.Services.AddOptions<DatabaseOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("DatabaseOptions").Bind(settings);
                });

            builder.Services.AddOptions<TvMazeApiOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("TvMazeApiOptions").Bind(settings);
                });
        }
    }
}