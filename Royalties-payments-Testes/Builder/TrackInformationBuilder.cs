using Royalties_payments.Models;
using Royalties_payments.Service;

namespace Royalties_payments_Testes.Builder;
public static class TrackInformationBuilder
{
    public static List<TrackInformation> CreateWithTwoTracks()
    {
        var trackData = TrackDataBuilder.CreateTwoTracks();
        var trackId1 = trackData.Tracks[0].Id;
        var trackId2 = trackData.Tracks[1].Id;
        var service = new TrackService(trackData);

        var trackInformation = service.GetTrackInformation([trackId1, trackId2]);
        return trackInformation;
    }

    public static List<TrackInformation> CreateWithOneTrack()
    {
        var trackData = TrackDataBuilder.CreateOneTrackWithOnePerformerAndOneComposer();
        var trackId = trackData.Tracks[0].Id;
        Console.WriteLine(trackData.Tracks[0].PerformersId);
        var service = new TrackService(trackData);

        var trackInformation = service.GetTrackInformation([trackId]);
        return trackInformation;
    }
}
