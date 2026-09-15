using Royalties_payments.Models;
using System.Text.RegularExpressions;

namespace Royalties_payments.Service;
public class RoyaltyCalculator
{
    public List<TrackInformation> TrackInformation { get; }
    public Subscriber Subscriber { get; }

    public RoyaltyCalculator(List<TrackInformation> trackInformation, Subscriber subscriber)
    {
        TrackInformation = trackInformation;
        Subscriber = subscriber;
    }
    public void Calculator()
    {
        decimal valorAssinatura = Subscriber.TotalPaidSubscription;
        decimal valorTotalComposer = valorAssinatura / 2;
        decimal valorTotalPerformer = valorAssinatura / 2;
        int qtdeTotalStreams = Subscriber.Streams.Count;

        Dictionary<string, decimal> ComposerRoyalties = CalculatorComposerRoyalties(valorAssinatura, valorTotalComposer, qtdeTotalStreams);
        Dictionary<string, decimal> PerformerRoyalties = CalculatorPerformersRoyalties(valorAssinatura, valorTotalPerformer, qtdeTotalStreams);
    }
    public void AddInDictionary(Dictionary<string, decimal> dicionario, string chave, decimal valor = 0)
    {
        if (dicionario.ContainsKey(chave))
        {
            dicionario[chave] += valor;
        }
        else
        {
            dicionario.Add(chave, valor);
        }
    }
    public void CalcularPercentual(int qtdeTotalStreams,Dictionary<string, decimal> dicionario, Dictionary<string, decimal> novoDicionario)
    {
        foreach (var item in dicionario)
        {
            decimal percentual = item.Value / qtdeTotalStreams;
            AddInDictionary(novoDicionario, item.Key, percentual);
        }
    }
    public void CalcularValor(decimal valorTotal, Dictionary<string,decimal> dicionario, Dictionary<string,decimal> novoDicionario)
    {
        foreach (var item in dicionario)
        {
            decimal valor = (item.Value * valorTotal);
            AddInDictionary(novoDicionario, item.Key, valor);
        }
    }
    public Dictionary<string,decimal> CalculoRoyalty(Dictionary<string,decimal> dicionario, Dictionary<string,decimal> novoDicionario)
    {
        foreach (var item in dicionario)
        {
            string[] persons = item.Key.Split("/").Select(c => c.Trim()).ToArray();
            int qtdePersons = persons.Length;
            decimal valuePerPerson = item.Value / qtdePersons;

            foreach (var person in persons)
            {
                AddInDictionary(novoDicionario, person, valuePerPerson);
            }
        }
        return novoDicionario;
    }
    public Dictionary<string, decimal> CalculatorPerformersRoyalties(decimal valorAssinatura, decimal valorTotalPerformer, int qtdeTotalStreams)
    {
        //Encontrar a quantidade de streamings que os performers da música receberam.
        Dictionary<string, decimal> performerStreams = new();

        foreach (var trackInformation in TrackInformation)
        {
            int qtdeStreamsTrack = Subscriber.Streams.Count(s => s.TrackId == trackInformation.Track.Id);
            string performersName = string.Join(" / ", trackInformation.Performers.Select(p => p.Name));
            AddInDictionary(performerStreams, performersName, qtdeStreamsTrack);
        }

        //calcula o percentual do performer
        Dictionary<string, decimal> percentualPerformer = new();
        CalcularPercentual(qtdeTotalStreams, performerStreams, percentualPerformer);

        //calcula o valor do performer
        Dictionary<string, decimal> valuesPerformers = new();
        CalcularValor(valorTotalPerformer, percentualPerformer, valuesPerformers);

        //performer Royalty
        Dictionary<string, decimal> performerRoyalty = new();
        performerRoyalty = CalculoRoyalty(valuesPerformers, performerRoyalty);

        return performerRoyalty;
    }
    public Dictionary<string,decimal> CalculatorComposerRoyalties(decimal valorAssinatura, decimal valorTotalComposer, int qtdeTotalStreams)
    {
        //Encontrar a quantidade de streamings que os compositores da música receberam.
        Dictionary<string, decimal> composersStreams = new();
        Dictionary<string, decimal> composerRoyalts = new();

        foreach (var trackInformation in TrackInformation)
        {
            int qtdeStreamsTrack = Subscriber.Streams.Count(s => s.TrackId == trackInformation.Track.Id);
            string composersName = string.Join(" / ", trackInformation.Composers.Select(c => c.Name));
            AddInDictionary(composersStreams, composersName, qtdeStreamsTrack);

            foreach (var composerName in trackInformation.Composers)
            {
                AddInDictionary(composerRoyalts, composerName.Name);
            }
        }

        //calcular o percentual dos compositores
        Dictionary<string, decimal> composersPercentual = new();
        CalcularPercentual(qtdeTotalStreams, composersStreams, composersPercentual);

        //Calcular o valor dos compositores juntos
        Dictionary<string, decimal> composersValues = new();
        CalcularValor(valorTotalComposer, composersPercentual, composersValues);

        //Calcular o valor de cada compositor, em separado
        composerRoyalts = CalculoRoyalty(composersValues, composerRoyalts);

        return composerRoyalts;
    }
}