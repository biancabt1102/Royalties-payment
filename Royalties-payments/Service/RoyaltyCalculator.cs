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

        Dictionary<Guid, decimal> ComposerRoyalties = CalculatorComposerRoyalties(valorTotalComposer, qtdeTotalStreams);
        Dictionary<Guid, decimal> PerformerRoyalties = CalculatorPerformersRoyalties(valorTotalPerformer, qtdeTotalStreams);
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

            decimal valuePerPerformer = valueTrack / qtdePerformersPerTrack;

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

            decimal valuePerComposer = valueTrack / qtdeComposerPerTrack;

            foreach (var composerId in trackInformation.Composition.ComposerId)
            {
                AddInDictionary(composerRoyalty, composerId, valuePerComposer);
            }
        }
        return composerRoyalty;
    }
}