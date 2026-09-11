namespace Royalties_payments.Models;
public class TrackData
{
    public List<Performer> Performers { get; set; } = [];
    public List<Composer> Composers { get; set; } = [];
    public List<Composition> Compositions { get; set; } = [];
    public List<Track> Tracks { get; set; } = [];
}