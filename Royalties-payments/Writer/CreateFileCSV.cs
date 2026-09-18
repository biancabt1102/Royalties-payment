using Royalties_payments.Models;
using Royalties_payments.Service;

namespace Royalties_payments.Writer;

public static class CreateFileCSV
{
    public static void CriarArquivo(string caminho, List<RoyaltyPayment> payments)
    {
        using (StreamWriter wr = new StreamWriter(caminho))
        {
            wr.WriteLine("IdRecebedor;TipoRoyalty;TotalRoyalties");

            foreach (var payment in payments)
            {
                wr.WriteLine($"{payment.ReceiverId};{payment.RoyaltyType};{payment.TotalRoyalties}");
            }
        }
    }
}