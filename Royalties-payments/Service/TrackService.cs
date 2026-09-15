using Royalties_payments.Models;

namespace Royalties_payments.Service;
public class TrackService
{
    private readonly TrackData _trackData;
    public TrackService(TrackData trackData)
    {
        _trackData = trackData;
    }

    public List<TrackInformation>? GetTrackInformation(List<Guid> trackIds)
    {
        var tracks = _trackData.Tracks.Where(t => trackIds.Contains(t.Id));

        var tracksInformation = new List<TrackInformation>();

        if (tracks is null)
        {
            throw new InvalidDataException("Não foi encontrado nenhuma informação de track.");
        }

        foreach (var track in tracks)
        {
            var composition = _trackData.Compositions.FirstOrDefault(c => c.Id == track.CompositionId);
            
            if (composition is null)
            {
                throw new InvalidDataException("Não foi encontrado nenhuma informação de composition");
            }

            var composers = _trackData.Composers.Where(c => composition.ComposerId.Contains(c.Id)).ToList();

            var performers = _trackData.Performers.Where(p => track.PerformersId.Contains(p.Id)).ToList();

            tracksInformation.Add(new TrackInformation(track, composition, composers, performers));
        }
        return tracksInformation;
    }
}