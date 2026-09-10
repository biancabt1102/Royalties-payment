using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Composition
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("publish_date")]
    public DateTime DatePublish { get; set; }
    [JsonPropertyName("composers_ids")]
    public List<Composer> Composers { get; set; }
}