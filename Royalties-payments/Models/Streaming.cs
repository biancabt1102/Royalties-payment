using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Streaming
{
    public Guid Id { get; set; }
    [JsonPropertyName("")]
    public List<Track> Tracks { get; set; }
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}