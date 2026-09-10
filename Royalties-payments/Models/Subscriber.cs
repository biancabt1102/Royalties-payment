using System.Text.Json.Serialization;

namespace Royalties_payments.Models;
public class Subscriber
{
    [JsonPropertyName("subscriber_id")]
    public Guid SubscribeId { get; set; }
    [JsonPropertyName("total_paid_subscription")]
    public double TotalPaidSubscription { get; set; }
    [JsonPropertyName("streams")]
    public List<Streaming> Streams { get; set; }
}