using Royalties_payments.Models;
using Royalties_payments.Writer;

namespace Royalties_payments_Testes.Integration;
public class CreateFileCSVIntegrationTest
{
    [Fact]
    public void CreateFile_MustCreateCSVCorrectlly()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");

        var receiverId = Guid.NewGuid();

        var payments = new List<RoyaltyPayment>
        {
            new RoyaltyPayment(receiverId, "Composer", 5.50m)
        };

        try
        {
            //Act
            CreateFileCSV.CriarArquivo(caminho, payments);
            //Assert
            Assert.True(File.Exists(caminho));
            var linhas = File.ReadAllLines(caminho);
            Assert.Equal("IdRecebedor;TipoRoyalty;TotalRoyalties", linhas[0]);
            Assert.Equal($"{receiverId};Composer;{5.50m.ToString()}", linhas[1]);

        }
        finally
        {
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
            }
        }
    }

    [Fact]
    public void CreateFile_MustCreateFileCSVWithData()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");

        var payments = new List<RoyaltyPayment>
        {
            new RoyaltyPayment(Guid.NewGuid(), "Composer", 10.50m)
        };

        //Act
        CreateFileCSV.CriarArquivo(caminho, payments);

        //Assert
        Assert.True(File.Exists(caminho));

        var linhas = File.ReadAllLines(caminho);
        Assert.Contains("IdRecebedor;TipoRoyalty;TotalRoyalties", linhas[0]);
        Assert.Contains(payments[0].ReceiverId.ToString(), linhas[1]);
        Assert.Contains("Composer", linhas[1]);
        Assert.Contains(10.50m.ToString(), linhas[1]);

        File.Delete(caminho);
    }

    [Fact]
    public void CreateFile_MustAddAllPayments()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");

        Guid receiver1 = Guid.NewGuid();
        Guid receiver2 = Guid.NewGuid();

        var payments = new List<RoyaltyPayment>
        {
            new RoyaltyPayment(receiver1, "Composer", 10.50m),
            new RoyaltyPayment(receiver2, "Performer", 5.25m)
        };

        //Act
        CreateFileCSV.CriarArquivo(caminho, payments);

        //Assert
        Assert.True(File.Exists(caminho));
        var linhas = File.ReadAllLines(caminho);

        Assert.Contains(receiver1.ToString(), linhas[1]);
        Assert.Contains(receiver2.ToString(), linhas[2]);

        File.Delete(caminho);
    }

    [Fact]
    public void CreateFile_MustThrowExceptionWhenPathIsInvalid()
    {
        //Arrange
        string caminhoInvalido = Path.Combine(Path.GetTempPath(), "invalid_path", $"{Guid.NewGuid()}.csv");
        var payments = new List<RoyaltyPayment>
        {
            new RoyaltyPayment(Guid.NewGuid(), "Composer", 10.50m)
        };

        //Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => CreateFileCSV.CriarArquivo(caminhoInvalido, payments));
    }

    [Fact]
    public void CreateFile_MustThrowExceptionWhenPathIsNull()
    {
        //Arrange
        string? caminhoNulo = null;
        var payments = new List<RoyaltyPayment>
        {
            new RoyaltyPayment(Guid.NewGuid(), "Composer", 10.50m)
        };
        //Act & Assert
        Assert.Throws<ArgumentNullException>(() => CreateFileCSV.CriarArquivo(caminhoNulo, payments));
    }

    [Fact]
    public void CreateFile_MustCreateHeaderWhenPaymentsListIsEmpty()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");
        var payments = new List<RoyaltyPayment>();

        //Act
        CreateFileCSV.CriarArquivo(caminho, payments);

        //Assert
        var linhas = File.ReadAllLines(caminho);
        Assert.Single(linhas);
        Assert.Equal("IdRecebedor;TipoRoyalty;TotalRoyalties", linhas[0]);
        File.Delete(caminho);
    }
}