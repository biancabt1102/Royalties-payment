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
    public List<Dictionary<Guid, decimal>> Calculate()
    {
        decimal valorAssinatura = Subscriber.TotalPaidSubscription;
        decimal valorTotalComposer = valorAssinatura / 2;
        decimal valorTotalPerformer = valorAssinatura / 2;
        int qtdeTotalStreams = Subscriber.Streams.Count;

        Dictionary<Guid, decimal> composerRoyalties = CalculateComposerRoyalties(valorTotalComposer, qtdeTotalStreams);
        Dictionary<Guid, decimal> performerRoyalties = CalculatePerformersRoyalties(valorTotalPerformer, qtdeTotalStreams);

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
    public void IterateTracks(Dictionary<Guid, decimal> dicionario, List<TrackInformation> trackInformation, Subscriber subscriber, decimal valuePerStream, string tipo)
    {
        foreach (var track in trackInformation)
        {
            int qtdeStreamTrack = subscriber.Streams.Count(s => s.TrackId == track.Track.Id);
            decimal valueTrack = valuePerStream * qtdeStreamTrack; 

            if (tipo.Equals("composer"))
            {
                int qtdeComposerPerTrack = track.Composition.ComposerId.Count;
                decimal valuePerComposer = Math.Round((valueTrack / qtdeComposerPerTrack), 2, MidpointRounding.ToEven);
                foreach (var composerId in track.Composition.ComposerId)
                {
                    AddInDictionary(dicionario, composerId, valuePerComposer);
                }
            }
            else
            {
                int qtdePerformersPerTrack = track.Track.PerformersId.Count;
                decimal valuePerPerformer = Math.Round((valueTrack / qtdePerformersPerTrack), 2, MidpointRounding.ToEven);
                foreach (var performerId in track.Track.PerformersId)
                {
                    AddInDictionary(dicionario, performerId, valuePerPerformer);
                }
            }
        }
    }
    public Dictionary<Guid, decimal> CalculatePerformersRoyalties(decimal valorTotalPerformer, int qtdeTotalStreams)
    {
        Dictionary<Guid, decimal> performerRoyalty = new();
        decimal valuePerStream = (valorTotalPerformer / qtdeTotalStreams);
        
        IterateTracks(performerRoyalty, TrackInformation, Subscriber, valuePerStream, "performer");
        return performerRoyalty;
    }
    public Dictionary<Guid,decimal> CalculateComposerRoyalties(decimal valorTotalComposer, int qtdeTotalStreams)
    {
        Dictionary<Guid, decimal> composerRoyalty = new();
        decimal valuePerStream = (valorTotalComposer / qtdeTotalStreams);
        
        IterateTracks(composerRoyalty, TrackInformation, Subscriber, valuePerStream, "composer");
        return composerRoyalty;
    }
}