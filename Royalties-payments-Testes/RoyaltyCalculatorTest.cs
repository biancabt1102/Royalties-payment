using Royalties_payments.Models;
using Royalties_payments.Service;
using Royalties_payments_Testes.Builder;
using Xunit.Sdk;

namespace Royalties_payments_Testes;
public class RoyaltyCalculatorTest
{
    [Fact]
    public void Calculate_ShouldCalculateRoyalties()
    {
        //Arrange
        var trackInformation = TrackInformationBuilder.CreateWithOneTrack();
        var subscriber = SubscribersBuilder.CreateOneSubscriberWithOneStream(trackInformation);
        var royalty = new RoyaltyCalculator(trackInformation, subscriber);

        //Act
        var result = royalty.Calculate();

        var composerResult = result[0];
        var performerResult = result[1];
        var composerId = trackInformation[0].Composition.ComposerId[0];
        var performerId = trackInformation[0].Track.PerformersId[0];

        //Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(12.5m, composerResult[composerId]);
        Assert.Equal(12.5m, performerResult[performerId]);
    }

    [Fact]
    public void Calculate_ShouldSumRoyaltiesFor_SamePerformerDiffTracksAndSameComposer()
    {
        //Arrange
	    var trackInformation = TrackInformationBuilder.CreateWithTwoTracks();
        var subscriber = SubscribersBuilder.CreateOneSubscriberWithTwoStreams(trackInformation);
        var royalty = new RoyaltyCalculator(trackInformation, subscriber);
        //Act
        var result = royalty.Calculate();

        var composerResult = result[0];
        var performerResult = result[1];

        var composerId1 = trackInformation[0].Composition.ComposerId[0];
        var performerId1 = trackInformation[0].Track.PerformersId[0];
        var composerId2 = trackInformation[1].Composition.ComposerId[0];
        var performerId2 = trackInformation[1].Track.PerformersId[0];

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(12.5m, composerResult[composerId1]);
        Assert.Equal(12.5m, performerResult[performerId1]);
        Assert.Equal(12.5m, composerResult[composerId2]);
        Assert.Equal(12.5m, performerResult[performerId2]);
    }
}