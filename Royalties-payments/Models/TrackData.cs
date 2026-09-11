using System.Text.Json.Serialization;
namespace Royalties_payments.Models;
public class TrackData
{
    [JsonPropertyName("performers")]
    public List<Performer> Performers { get; set; } = [];
    [JsonPropertyName("composers")]
    public List<Composer> Composers { get; set; } = [];
    [JsonPropertyName("compositions")]
    public List<Composition> Compositions { get; set; } = [];
    [JsonPropertyName("tracks")]
    public List<Track> Tracks { get; set; } = [];
}