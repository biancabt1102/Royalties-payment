using Royalties_payments.Models;

namespace Royalties_payments.Service;
public class TrackService
{
    private readonly TrackData _trackData;
    public TrackService(TrackData trackData)
    {
        _trackData = trackData;
    }

    public Track? getTrackById(Guid trackId)
    {
        return _trackData.Tracks.FirstOrDefault(x => x.Id == trackId);
    }
}
