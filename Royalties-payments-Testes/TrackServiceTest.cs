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
        //preparar o trackData e trackService
        var trackData = TrackDataBuilder.CreateOneTrackWithOnePerformerAndOneComposer();
        var service = new TrackService(trackData);
        var trackId = trackData.Tracks.First().Id;

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