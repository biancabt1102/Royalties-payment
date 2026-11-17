using Royalties_payments.Models;
using System.Text.Json;

namespace Royalties_payments_Testes.Builder;
public static class SubscribersBuilder
{
    public static List<Subscriber> Create(string path)
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json)) throw new InvalidOperationException("O arquivo está vazio.");
        List<Subscriber>? subscribers = JsonSerializer.Deserialize<List<Subscriber>>(json);
        return subscribers;
    }

    public static Subscriber CreateOneSubscriberWithOneStream(List<TrackInformation> trackInformation)
    {
        var subscriber = new Subscriber
        {
            SubscriberId = Guid.NewGuid(),
            Streams = new List<Streaming>
            {
                new Streaming
                {
                    TrackId = trackInformation[0].Track.Id
                }
            },
            TotalPaidSubscription = 25
        };
        return subscriber;
    }

    public static Subscriber CreateOneSubscriberWithTwoStreams(List<TrackInformation> trackInformation)
    {
        var subscriber = new Subscriber
        {
            SubscriberId = Guid.NewGuid(),
            Streams = new List<Streaming>
            {
                new Streaming
                {
                    TrackId = trackInformation[0].Track.Id
                },
                new Streaming
                {
                    TrackId = trackInformation[1].Track.Id
                }
            },
            TotalPaidSubscription = 25
        };
        return subscriber;
    }
}