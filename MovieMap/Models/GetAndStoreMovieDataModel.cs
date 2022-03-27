using System.Text.Json.Serialization;

namespace MovieMap
{
    public record GetAndStoreMovieDataModel(
        [property: JsonPropertyName("movieName")] string MovieName
    );
}
