using Royalties_payments.Models;
using System.Text.Json;
namespace Royalties_payments.Reader;
public static class SubscriberReader
{
    public static List<Subscriber> Read(string filePath)
    {
        string json = File.ReadAllText(filePath);

        List<Subscriber>? subscribers = JsonSerializer.Deserialize<List<Subscriber>>(json);

        if (subscribers is null)
        {
            throw new InvalidOperationException("Não foi possível ler o arquivo de subscribers.");
        }
        return subscribers;
    }
}