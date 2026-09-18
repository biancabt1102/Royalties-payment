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
    public List<Dictionary<Guid, decimal>> Calculator()
    {
        decimal valorAssinatura = Subscriber.TotalPaidSubscription;
        decimal valorTotalComposer = valorAssinatura / 2;
        decimal valorTotalPerformer = valorAssinatura / 2;
        int qtdeTotalStreams = Subscriber.Streams.Count;

        Dictionary<Guid, decimal> composerRoyalties = CalculatorComposerRoyalties(valorTotalComposer, qtdeTotalStreams);
        Dictionary<Guid, decimal> performerRoyalties = CalculatorPerformersRoyalties(valorTotalPerformer, qtdeTotalStreams);

        List<Dictionary<Guid, decimal>> dicionarios = new();
        dicionarios.Add(composerRoyalties);
        dicionarios.Add(performerRoyalties);
        return dicionarios;
    }
    public void AddInDictionary(Dictionary<Guid, decimal> dicionario, Guid chave, decimal valor)
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
    public Dictionary<Guid, decimal> CalculatorPerformersRoyalties(decimal valorTotalPerformer, int qtdeTotalStreams)
    {
        Dictionary<Guid, decimal> performerRoyalty = new();
        decimal valuePerStream = (valorTotalPerformer / qtdeTotalStreams);

        foreach (var trackInformation in TrackInformation)
        {
            int qtdeStreamTrack = Subscriber.Streams.Count(s => s.TrackId == trackInformation.Track.Id);

            int qtdePerformersPerTrack = trackInformation.Track.PerformersId.Count;

            decimal valueTrack = valuePerStream * qtdeStreamTrack;

            decimal valuePerPerformer = Math.Round((valueTrack / qtdePerformersPerTrack), 2, MidpointRounding.ToEven);

            foreach (var performerId in trackInformation.Track.PerformersId)
            {
                AddInDictionary(performerRoyalty, performerId, valuePerPerformer);
            }
        }
        return performerRoyalty;
    }
    public Dictionary<Guid,decimal> CalculatorComposerRoyalties(decimal valorTotalComposer, int qtdeTotalStreams)
    {
        Dictionary<Guid, decimal> composerRoyalty = new();
        decimal valuePerStream = (valorTotalComposer / qtdeTotalStreams);

        foreach (var trackInformation in TrackInformation)
        {
            int qtdeStreamTrack = Subscriber.Streams.Count(s => s.TrackId == trackInformation.Track.Id);

            int qtdeComposerPerTrack = trackInformation.Composition.ComposerId.Count;

            decimal valueTrack = valuePerStream * qtdeStreamTrack;

            decimal valuePerComposer = Math.Round((valueTrack / qtdeComposerPerTrack), 2, MidpointRounding.ToEven);

            foreach (var composerId in trackInformation.Composition.ComposerId)
            {
                AddInDictionary(composerRoyalty, composerId, valuePerComposer);
            }
        }
        return composerRoyalty;
    }
}