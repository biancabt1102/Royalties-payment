namespace Royalties_payments.Models;
public class RoyaltyPayment
{
    public Guid ReceiverId { get; }
    public string RoyaltyType { get; }
    public decimal TotalRoyalties { get; }
    public RoyaltyPayment(Guid receiverId, string royaltyType, decimal totalRoyalties)
    {
        ReceiverId = receiverId;
        RoyaltyType = royaltyType;
        TotalRoyalties = totalRoyalties;
    }
}
