using Royalties_payments.Models;
using Royalties_payments.Service;
using Royalties_payments.Writer;
using System.Text.Json;
public class Program
{
    public static void Main(string[] args)
    {
        //caminho do arquivo json
        string trackPath = args[0];
        string subscribersPath = args[1];

        try
        {
            //leitura do arquivo
            string jsonTrack = File.ReadAllText(trackPath);
            TrackData? trackData = JsonSerializer.Deserialize<TrackData>(jsonTrack);
            string jsonSubscribers = File.ReadAllText(subscribersPath);
            List<Subscriber>? subscribers = JsonSerializer.Deserialize<List<Subscriber>>(jsonSubscribers);

            //Buscando as informações dos arquivos json
            var subscriber = subscribers[0];
            var streaming = subscriber.Streams[0];
            var trackIds = subscriber.Streams.Select(s => s.TrackId).ToList();

            TrackService trackService = new TrackService(trackData);
            var trackInformation = trackService.GetTrackInformation(trackIds);

            var royaltyCalculator = new RoyaltyCalculator(trackInformation, subscriber);

            RoyaltyPaymentService royaltyService = new RoyaltyPaymentService(royaltyCalculator);
            List<RoyaltyPayment> payments = royaltyService.GeneratePayment();

            CreateFileCSV.CriarArquivo("Royalties-payment.csv", payments);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao desserializar o arquivo json.", ex);
        }
    }
}