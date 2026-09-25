using Royalties_payments.Models;
using Royalties_payments.Service;
using Royalties_payments_Testes.Builder;
using Xunit.Sdk;

namespace Royalties_payments_Testes;
public class RoyaltyCalculatorTest
{
    [Fact]
    public void Calculator_MustCalculateRoyalties()
    {
        //Arrange
        var trackInformation = TrackInformationBuilder.Create();
        var subscriber = new Subscriber
        {
            SubscriberId = Guid.NewGuid(),
            Streams = new List<Streaming>
            {
                new Streaming
                {
                    TrackId = trackInformation[0].Track.Id
                }
            },
            TotalPaidSubscription = 25
        };
        var royalty = new RoyaltyCalculator(trackInformation, subscriber);

        //Act
        var result = royalty.Calculator();

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
    public void Calculator_MustSumRoyaltiesForSameReceiverInDifferentTracks()
    {
        //Arrange
	    var trackInformation = TrackInformationBuilder.Create();
        var subscriber = new Subscriber
        {
            SubscriberId = Guid.NewGuid(),
            Streams = new List<Streaming>
            {
                new Streaming
                {
                    TrackId = trackInformation[0].Track.Id
                },
                new Streaming
                {
                    TrackId = trackInformation[1].Track.Id
                }
            },
            TotalPaidSubscription = 25
        };
        var royalty = new RoyaltyCalculator(trackInformation, subscriber);
        //Act
        var result = royalty.Calculator();

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