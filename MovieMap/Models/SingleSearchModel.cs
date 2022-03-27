using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieMap
{

    public record Rating(
        [property: JsonPropertyName("average")] double Average
    );

    public record SingleSearchModel(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("genres")] IReadOnlyList<string> Genres,
        [property: JsonPropertyName("rating")] Rating Rating
    );
}
