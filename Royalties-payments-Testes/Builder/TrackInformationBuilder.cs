using Royalties_payments.Models;
using Royalties_payments.Service;

namespace Royalties_payments_Testes.Builder;
public static class TrackInformationBuilder
{
    public static List<TrackInformation> Create()
    {
        var trackId1 = Guid.NewGuid();
        var trackId2 = Guid.NewGuid();
        var performerId = Guid.NewGuid();
        var composerId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();

        var track1 = new Track
        {
            Id = trackId1,
            Title = "Test Track 1",
            PerformersId = [performerId],
            CompositionId = compositionId
        };

        var track2 = new Track
        {
            Id = trackId2,
            Title = "Test Track 2",
            PerformersId = [performerId],
            CompositionId = compositionId
        };

        var performer = new Performer
        {
            Id = performerId,
            Name = "Test Performer"
        };

        var composer = new Composer
        {
            Id = composerId,
            Name = "Test Composer"
        };

        var composition = new Composition
        {
            Id = compositionId,
            Title = "Test Composition",
            ComposerId = [composerId]
        };

        var trackData = new TrackData
        {
            Tracks = [track1, track2],
            Performers = [performer],
            Composers = [composer],
            Compositions = [composition]
        };

        var service = new TrackService(trackData);

        var trackInformation = service.GetTrackInformation([trackId1, trackId2]);

        return trackInformation;
    }
}
