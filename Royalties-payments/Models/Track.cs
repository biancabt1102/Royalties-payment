using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Track
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("composition_id")]
    public Guid CompositionId { get; set; }
    [JsonPropertyName("performers_ids")]
    public List<Guid> PerformersId { get; set; }
    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; }
}