using Royalties_payments.Models;
using System.Text.Json;

namespace Royalties_payments.Reader;
/// <summary>
/// Ler o JSON de metadados e transforma-lo em TrackData, com as informações conforme o arquivo.
/// </summary>
public static class TrackReader
{
    public static TrackData Read(string filePath)
    {
        string json = File.ReadAllText(filePath);

        TrackData? trackData = JsonSerializer.Deserialize<TrackData>(json);

        if (trackData is null)
        {
            throw new InvalidOperationException("Não foi possível ler o arquivo de tracks.");
        }
        return trackData;
    }
}