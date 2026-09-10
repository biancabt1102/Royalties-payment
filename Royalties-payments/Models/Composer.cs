using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Composer
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}