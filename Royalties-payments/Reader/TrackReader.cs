using Royalties_payments.Models;
using System.Text.Json;

namespace Royalties_payments.Reader;
public static class TrackReader
{
    public static TrackData Read(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            TrackData? trackData = JsonSerializer.Deserialize<TrackData>(json);
            return trackData;

        }catch (JsonException ex)
        {
            throw new InvalidOperationException("Erro ao desserializar o arquivo de tracks.",ex);
        }
    }
}