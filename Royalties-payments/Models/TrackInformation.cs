namespace Royalties_payments.Models;
public class TrackInformation
{
    public Track Track { get; }
    public Composition Composition { get; }
    public List<Composer> Composers { get; }
    public List<Performer> Performers { get; }
    public TrackInformation(Track track, Composition composition, List<Composer> composers, List<Performer> performers)
    {
        Track = track;
        Composition = composition;
        Composers = composers;
        Performers = performers;
    }
}