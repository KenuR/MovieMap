using System.Text.Json.Serialization;

namespace MovieMap.Models
{
    public record GetHighestRatedModel
    (
        [property: JsonPropertyName("genre")] string Genre
    );
}
