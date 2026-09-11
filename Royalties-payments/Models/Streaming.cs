using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Streaming
{
    [JsonPropertyName("track_id")]
    public Guid TrackId { get; set; }
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}