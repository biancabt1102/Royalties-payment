using Royalties_payments.Models;

namespace Royalties_payments.Service;
public class RoyaltyPaymentService
{
    private readonly RoyaltyCalculator _royaltyCalculator;
    public RoyaltyPaymentService(RoyaltyCalculator royaltyCalculator)
    {
        _royaltyCalculator = royaltyCalculator;
    }

    public List<RoyaltyPayment> GeneratePayment()
    {
        var payments = new List<RoyaltyPayment>();

        List<Dictionary<Guid, decimal>> dicionarios = _royaltyCalculator.Calculator();

        Dictionary<Guid, decimal> composerRoyalties = dicionarios[0];
        Dictionary<Guid, decimal> performerRoyalties = dicionarios[1];

        foreach (var composer in composerRoyalties)
        {
            payments.AddRange(new RoyaltyPayment(composer.Key, "composer",composer.Value));
        }

        foreach (var performer in performerRoyalties)
        {
            payments.AddRange(new RoyaltyPayment(performer.Key, "performer", performer.Value));
        }

        return payments;
    }
}