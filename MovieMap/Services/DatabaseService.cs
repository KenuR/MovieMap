using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieMap.Config;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace MovieMap.Services
{
    public interface IDatabaseService
    {
        MovieModel GetHighestRatedPerGenre(string genre);
        void StoreMovie(SingleSearchModel model);
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseOptions _dbOptions;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(
            IOptions<DatabaseOptions> options,
            ILogger<DatabaseService> logger
            )
        {
            _dbOptions = options.Value;
            _logger = logger;
        }
        public void StoreMovie(SingleSearchModel model)
        {
            using var conn = new SqlConnection(_dbOptions.DbConnectionString);

            conn.Open();

            foreach (var genre in model.Genres.Select(g => g.ToLower()))
            {
                conn.Query($"INSERT INTO dbo.Genre values(@MovieId, @GenreName)", new { MovieId = model.Id, GenreName = genre });
            }

            var movieExists = conn.QuerySingleOrDefault<int?>("SELECT 1 FROM dbo.Movie WHERE MovieId = @MovieId", new { MovieId = model.Id });

            if(movieExists != 1)
            {
                conn.Query("INSERT INTO dbo.Movie values(@MovieId, @Title, @Rating)",
                    new { MovieId = model.Id, Title = model.Name, Rating = model.Rating.Average });
            }
            else
            {
                _logger.LogInformation("Movie was already added to the database");
            }
        }

        public MovieModel GetHighestRatedPerGenre(string genre)
        {
            using var conn = new SqlConnection(_dbOptions.DbConnectionString);

            conn.Open();

            var highestRatedMovie = conn.QuerySingleOrDefault<MovieModel>(@"
                select top 1 
                    a.MovieId, a.Title, a.Rating 
                from Movie a
                left join Genre b on a.movieId = b.movieId
                where b.GenreName = @GenreName
                order by Rating desc
                ", new { GenreName = genre });


            if(highestRatedMovie == null)
            {
                _logger.LogInformation($"No movie with genre '{genre}' was found.");
            }

            return highestRatedMovie;
        }
    }
    public record MovieModel(int MovieId, string Title, decimal Rating);
}
