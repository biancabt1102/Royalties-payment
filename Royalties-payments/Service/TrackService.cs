using Royalties_payments.Models;

namespace Royalties_payments.Service;
public class TrackService
{
    private readonly TrackData _trackData;
    public TrackService(TrackData trackData)
    {
        _trackData = trackData;
    }

    public Track? GetTrackById(Guid trackId)
    {
        return _trackData.Tracks.FirstOrDefault(x => x.Id == trackId);
    }

    public Composition? GetComposition(Track track)
    {
        return _trackData.Compositions.FirstOrDefault(c => c.Id == track.CompositionId);
    }

    public List<Composer> GetComposers(Composition composition)
    {
        return _trackData.Composers.Where(c => composition.ComposerId.Contains(c.Id)).ToList();
    }

    public List<Performer> GetPerformers(Track track)
    {
        return _trackData.Performers.Where(p => track.PerformersId.Contains(p.Id)).ToList();
    }
}
