using Royalties_payments.Models;
using Royalties_payments.Service;
using Royalties_payments_Testes.Builder;
namespace Royalties_payments_Testes;
public class TrackServiceTest
{
    [Fact]
    public void GetTrackInformation_ShouldReturnFoundTrack()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateOneTrackWithOnePerformerAndOneComposer();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks.First().Id;
        //Act
        var result = service.GetTrackInformation([trackId]);
        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(trackId, result[0].Track.Id);
        Assert.Equal("Music Test", result[0].Track.Title);
    }

    [Fact]
    public void GetTrackInformation_ShouldReturnAllComposersOfTheTrack()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateOneTrackWithTwoComposer();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks[0].Id;
        var composerId1 = trackData.Composers[0].Id;
        var composerId2 = trackData.Composers[1].Id;
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
    public void GetTrackInformation_ShouldReturnAllPerformersOfTheTrack()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateOneTrackWithTwoPerformers();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks.First().Id;
        var performerId1 = trackData.Performers[0].Id;
        var performerId2 = trackData.Performers[1].Id;
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
    public void GetTrackInformation_ShouldReturnOneCompositionOfTheTrack()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateOneTrackWithOnePerformerAndOneComposer();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks[0].Id;
        var compositionId = trackData.Compositions[0].Id;
        //Act
        var result = service.GetTrackInformation([trackId]);
        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(compositionId, result[0].Composition.Id);
    }

    [Fact]
    public void GetTrackInformation_ShouldReturnSameComposerDifferentTracks()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateSameComposerDifferentPerformersDifferentTracks();
        var service = new TrackService(trackData);
        var trackId1 = trackData.Tracks[0].Id;
        var trackId2 = trackData.Tracks[1].Id;
        var composerId = trackData.Composers[0].Id;
        //Act
        var result = service.GetTrackInformation([trackId1, trackId2]);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(composerId, result[0].Composers[0].Id);
        Assert.Equal(composerId, result[1].Composers[0].Id);
    }

    [Fact]
    public void GetTrackInformation_ShouldReturnSamePerformerDifferentComposersDifferentTracks()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateSamePerformerDifferentComposersDifferentTracks();
        var service = new TrackService(trackData);
        var trackId1 = trackData.Tracks[0].Id;
        var trackId2 = trackData.Tracks[1].Id;
        var performerId = trackData.Performers[0].Id;
        //Act
        var result = service.GetTrackInformation([trackId1, trackId2]);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(performerId, result[0].Performers[0].Id);
        Assert.Equal(performerId, result[1].Performers[0].Id);
    }

    [Fact]
    public void GetTrackInformation_ShouldReturnDifferentComposersForSameTrack()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateDifferentComposerSameTrack();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks[0].Id;
        var composerId1 = trackData.Composers[0].Id;
        var composerId2 = trackData.Composers[1].Id;
        //Act
        var result = service.GetTrackInformation([trackId]);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(composerId1, result[0].Composers[0].Id);
        Assert.Equal(composerId2, result[0].Composers[1].Id);
        Assert.Equal(2, result[0].Composers.Count);
    }

    [Fact]
    public void GetTrackInformation_ShouldReturnDifferentPerformersForSameTrack()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateDifferentPerformersSameTrack();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks[0].Id;
        var performerId1 = trackData.Performers[0].Id;
        var performerId2 = trackData.Performers[1].Id;
        //Act
        var result = service.GetTrackInformation([trackId]);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(performerId1, result[0].Performers[0].Id);
        Assert.Equal(performerId2, result[0].Performers[1].Id);
        Assert.Equal(2, result[0].Performers.Count);
    }

    [Fact]
    public void GetTrackInformation_ShouldThrowException_WhenCompositionNotFound()
    {
        //Arrange
        var trackData = TrackDataBuilder.CreateOneTrackWithoutComposition();
        var trackId = trackData.Tracks.First().Id;
        var service = new TrackService(trackData);
        //Act
        var exception = Assert.Throws<InvalidDataException>(() => service.GetTrackInformation([trackId]));
        //Assert
        Assert.Equal("Não foi encontrado nenhuma informação de composition", exception.Message);
    }

    [Fact]
    public void GetTrackInformation_ShouldThrowException_WhenTrackNotFound()
    {
        //Arrange
        var trackId = Guid.NewGuid();
        var trackData = new TrackData { Tracks = [] };
        var service = new TrackService(trackData);
        //Act
        var exception = Assert.Throws<InvalidDataException>(() => service.GetTrackInformation([trackId]));
        //Assert
        Assert.Equal("Não foi encontrado nenhuma informação de track.", exception.Message);
    }
}