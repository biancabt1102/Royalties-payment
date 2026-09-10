namespace Royalties_payments.Writer;

public static class CreateFileCSV
{
    public static void CriarArquivo(string caminho)
    {
        using (StreamWriter wr = new StreamWriter(caminho))
        {
            wr.WriteLine("IdRecebedor;TipoRoyalty;TotalRoyalties");
            wr.WriteLine("");
        }
    }
}
