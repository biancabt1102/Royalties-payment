using Royalties_payments.Models;
using Royalties_payments.Service;
namespace Royalties_payments_Testes;
public class TrackServiceTest
{
    [Fact]
    public void GetTrackInformation_MustReturnFoundTrack()
    {
        //Arrange
        //preparar o trackData e trackService
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
        var service = new TrackService(trackData);

        //Act
        //chamar o getTrackInformation com uma lista de trackIds
        var result = service.GetTrackInformation([trackId]);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(trackId, result[0].Track.Id);
        Assert.Equal("Music Test", result[0].Track.Title);
    }

    [Fact]
    public void GetTrackInformation_MustReturnAllComposersOfTheTrack()
    {
        //Arrange
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
        var service = new TrackService(trackData);

        //Act
        var result = service.GetTrackInformation([trackId]);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(2, result[0].Composers.Count);
        Assert.Contains(result[0].Composers, c => c.Id == composerId1);
        Assert.Contains(result[0].Composers, c => c.Id == composerId2);
    }

    [Fact]
    public void GetTrackInformation_MustReturnAllPerformersOfTheTrack()
    {
        //Arrange
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
        var service = new TrackService(trackData);

        //Act
        var result = service.GetTrackInformation([trackId]);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(2, result[0].Performers.Count);
        Assert.Contains(result[0].Performers, p => p.Id == performerId1);
        Assert.Contains(result[0].Performers, p => p.Id == performerId2);
    }

    [Fact]
    public void GetTrackInformation_MustReturnTheCompositionOfTheTrack()
    {
        //Arrange
        var trackId = Guid.NewGuid();
        var compositionId = Guid.NewGuid();
        var composerId = Guid.NewGuid();
        var performerId = Guid.NewGuid();
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
        var performer1 = new Performer
        {
            Id = performerId,
            Name = "Performer Test 1"
        };
        var trackData = new TrackData
        {
            Tracks = [track],
            Compositions = [composition],
            Performers = [performer1]
        };
        var service = new TrackService(trackData);

        //Act
        var result = service.GetTrackInformation([trackId]);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(compositionId, result[0].Composition.Id);
    }

    [Fact]
    public void GetTrackInformation_MustThrowExceptionWhenCompositionNotFound()
    {
        //Arrange
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
        var service = new TrackService(trackData);

        //Act
        var exception = Assert.Throws<InvalidDataException>(() => service.GetTrackInformation([trackId]));

        //Assert
        Assert.Equal("Não foi encontrado nenhuma informação de composition", exception.Message);
    }

    [Fact]
    public void GetTrackInformation_MustThrowExceptionWhenTrackNotFound()
    {
        //Arrange
        var trackId = Guid.NewGuid();
        var trackData = new TrackData
        {
            Tracks = []
        };
        var service = new TrackService(trackData);

        //Act
        var exception = Assert.Throws<InvalidDataException>(() => service.GetTrackInformation([trackId]));

        //Assert
        Assert.Equal("Não foi encontrado nenhuma informação de track.", exception.Message);
    }
}