namespace Royalties_payments.Models;
public class RoyaltyPayment
{
    public Guid ReceiverId { get; set; }
    public string RoyaltyType { get; set; }
    public decimal TotalRoyalties { get; set; }
}
