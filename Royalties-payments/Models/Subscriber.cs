using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Subscriber
{
    [JsonPropertyName("subscriber_id")]
    public Guid SubscriberId { get; set; }
    [JsonPropertyName("total_paid_subscription")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TotalPaidSubscription { get; set; }
    [JsonPropertyName("streams")]
    public List<Streaming> Streams { get; set; } = [];
}