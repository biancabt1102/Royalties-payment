using Royalties_payments.Models;
using System.Text.Json;
namespace Royalties_payments.Reader;
public static class SubscriberReader
{
    public static List<Subscriber> Read(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            List<Subscriber>? subscribers = JsonSerializer.Deserialize<List<Subscriber>>(json);
            return subscribers;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Erro ao desserializar o arquivo de subscribers.", ex);
        }
    }
}