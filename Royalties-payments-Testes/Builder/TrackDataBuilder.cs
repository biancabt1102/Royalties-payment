using Royalties_payments.Models;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Royalties_payments_Testes.Builder;
public static class TrackDataBuilder
{
    public static TrackData CreateTwoTracks()
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
        return trackData;
    }

    public static TrackData CreateOneTrackWithOnePerformerAndOneComposer()
    {
        var trackId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var performerId = Guid.NewGuid();
        var composerId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Title = "Music Test",
            CompositionId = compositionId,
            PerformersId = [performerId]
        };
        var composition = new Composition
        {
            Id = compositionId,
            Title = "Composition Test",
            ComposerId = [composerId]
        };
        var performer = new Performer
        {
            Id = performerId,
            Name = "Performer Test"
        };
        var composer = new Composer
        {
            Id = composerId,
            Name = "Composer Test"
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = [composition],
            Performers = [performer],
            Composers = [composer]
        };
        return trackData;
    }

    public static TrackData CreateOneTrackWithTwoComposer()
    {
        var trackId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var composerId1 = Guid.NewGuid();
        var composerId2 = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Title = "Music Test",
            CompositionId = compositionId
        };
        var composition = new Composition
        {
            Id = compositionId,
            Title = "Composition Test",
            ComposerId = [composerId1, composerId2]
        };
        var composer1 = new Composer
        {
            Id = composerId1,
            Name = "Composer Test 1"
        };
        var composer2 = new Composer
        {
            Id = composerId2,
            Name = "Composer Test 2"
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = [composition],
            Composers = [composer1, composer2]
        };
        return trackData;
    }

    public static TrackData CreateOneTrackWithTwoPerformers()
    {
        var trackId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var composerId = Guid.NewGuid();
        var performerId1 = Guid.NewGuid();
        var performerId2 = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Title = "Music Test",
            CompositionId = compositionId,
            PerformersId = [performerId1, performerId2]
        };
        var composition = new Composition
        {
            Id = compositionId,
            Title = "Composition Test",
            ComposerId = [composerId]
        };
        var performer1 = new Performer
        {
            Id = performerId1,
            Name = "Performer Test 1"
        };
        var performer2 = new Performer
        {
            Id = performerId2,
            Name = "Performer Test 2"
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = [composition],
            Performers = [performer1, performer2]
        };
        return trackData;
    }

    public static TrackData CreateOneTrackWithoutComposition()
    {
        var trackId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Title = "Music Test",
            CompositionId = compositionId
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = []
        };
        return trackData;
    }

    public static TrackData? ReadFile(string path)
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json)) throw new InvalidOperationException("O arquivo está vazio.");
        TrackData? trackData = JsonSerializer.Deserialize<TrackData>(json);
        return trackData;
    }

    public static TrackData CreateSameComposerDifferentPerformersDifferentTracks()
    {
        var trackId1 = Guid.NewGuid();
        var trackId2 = Guid.NewGuid();
        var composerId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var performerId1 = Guid.NewGuid();
        var performerId2 = Guid.NewGuid();

        var track1 = new Track
        {
            Id = trackId1,
            Title = "Test Track 1",
            PerformersId = [performerId1],
            CompositionId = compositionId
        };
        var track2 = new Track
        {
            Id = trackId2,
            Title = "Test Track 2",
            PerformersId = [performerId2],
            CompositionId = compositionId
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
        var performer1 = new Performer
        {
            Id = performerId1,
            Name = "Test Performer"
        };
        var performer2 = new Performer
        {
            Id = performerId2,
            Name = "Test Performer 2"
        };
        var trackData = new TrackData
        {
            Tracks = [track1, track2],
            Compositions = [composition],
            Performers = [performer1, performer2],
            Composers = [composer]
        };
        return trackData;
    }

    public static TrackData CreateSamePerformerDifferentComposersDifferentTracks()
    {
        var trackId1 = Guid.NewGuid();
        var trackId2 = Guid.NewGuid();
        var composerId1 = Guid.NewGuid();
        var composerId2 = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var performerId = Guid.NewGuid();

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
        var composer1 = new Composer
        {
            Id = composerId1,
            Name = "Test Composer"
        };
        var composer2 = new Composer
        {
            Id = composerId2,
            Name = "Test Composer 2"
        };
        var composition = new Composition
        {
            Id = compositionId,
            Title = "Test Composition",
            ComposerId = [composerId1]
        };
        var performer = new Performer
        {
            Id = performerId,
            Name = "Test Performer"
        };
        var trackData = new TrackData
        {
            Tracks = [track1, track2],
            Compositions = [composition],
            Performers = [performer],
            Composers = [composer1, composer2]
        };
        return trackData;
    }

    public static TrackData CreateDifferentComposerSameTrack()
    {
        var trackId = Guid.NewGuid();
        var compositionId1 = Guid.NewGuid();
        var compositionId2 = Guid.NewGuid();
        var composerId1 = Guid.NewGuid();
        var composerId2 = Guid.NewGuid();
        var performerId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Title = "Test Track",
            PerformersId = [performerId],
            CompositionId = compositionId1
        };
        var composition1 = new Composition
        {
            Id = compositionId1,
            Title = "Test Composition 1",
            ComposerId = [composerId1, composerId2]
        };
        var composer1 = new Composer
        {
            Id = composerId1,
            Name = "Test Composer 1"
        };
        var composer2 = new Composer
        {
            Id = composerId2,
            Name = "Test Composer 2"
        };
        var performer = new Performer
        {
            Id = performerId,
            Name = "Test Performer"
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = [composition1],
            Performers = [performer],
            Composers = [composer1, composer2]
        };
        return trackData;
    }

    public static TrackData CreateDifferentPerformersSameTrack()
    {
        var trackId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var composerId = Guid.NewGuid();
        var performerId1 = Guid.NewGuid();
        var performerId2 = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Title = "Test Track",
            PerformersId = [performerId1, performerId2],
            CompositionId = compositionId
        };
        var composition = new Composition
        {
            Id = compositionId,
            Title = "Test Composition",
            ComposerId = [composerId]
        };
        var composer = new Composer
        {
            Id = composerId,
            Name = "Test Composer"
        };
        var performer1 = new Performer
        {
            Id = performerId1,
            Name = "Test Performer 1"
        };
        var performer2 = new Performer
        {
            Id = performerId2,
            Name = "Test Performer 2"
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = [composition],
            Performers = [performer1, performer2],
            Composers = [composer]
        };
        return trackData;
    }
}