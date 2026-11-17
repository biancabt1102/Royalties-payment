using Royalties_payments.Models;
using Royalties_payments.Writer;

namespace Royalties_payments_Testes.Integration;
public class CreateFileCSVIntegrationTest
{
    [Fact]
    public void CreateFile_ShouldGenerateValidCSV()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");
        var receiverId = Guid.NewGuid();
        var payments = new List<RoyaltyPayment> { new RoyaltyPayment(receiverId, "Composer", 5.50m) };
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
            if (File.Exists(caminho)) File.Delete(caminho);
        }
    }

    [Fact]
    public void CreateFile_ShouldWritePaymentsToCSV()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");
        var payments = new List<RoyaltyPayment> { new RoyaltyPayment(Guid.NewGuid(), "Composer", 10.50m) };
        try
        {
            //Act
            CreateFileCSV.CriarArquivo(caminho, payments);

            //Assert
            Assert.True(File.Exists(caminho));

            var linhas = File.ReadAllLines(caminho);
            Assert.Contains("IdRecebedor;TipoRoyalty;TotalRoyalties", linhas[0]);
            Assert.Contains(payments[0].ReceiverId.ToString(), linhas[1]);
            Assert.Contains("Composer", linhas[1]);
            Assert.Contains(10.50m.ToString(), linhas[1]);
        }
        finally
        {
            if(File.Exists(caminho)) File.Delete(caminho);
        }
    }

    [Fact]
    public void CreateFile_ShouldWriteAllPaymentsToCSV()
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
        try
        {
            //Act
            CreateFileCSV.CriarArquivo(caminho, payments);

            //Assert
            Assert.True(File.Exists(caminho));
            var linhas = File.ReadAllLines(caminho);

            Assert.Contains(receiver1.ToString(), linhas[1]);
            Assert.Contains(receiver2.ToString(), linhas[2]);
        }
        finally
        {
            if(File.Exists(caminho)) File.Delete(caminho);
        }
    }

    [Fact]
    public void CreateFile_ShouldThrowExceptionWhenPathIsInvalid()
    {
        //Arrange
        string caminhoInvalido = Path.Combine(Path.GetTempPath(), "invalid_path", $"{Guid.NewGuid()}.csv");
        var payments = new List<RoyaltyPayment> { new RoyaltyPayment(Guid.NewGuid(), "Composer", 10.50m) };

        //Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => CreateFileCSV.CriarArquivo(caminhoInvalido, payments));
    }

    [Fact]
    public void CreateFile_ShouldThrowExceptionWhenPathIsNull()
    {
        //Arrange
        string? caminhoNulo = null;
        var payments = new List<RoyaltyPayment> { new RoyaltyPayment(Guid.NewGuid(), "Composer", 10.50m) };
        //Act & Assert
        Assert.Throws<ArgumentNullException>(() => CreateFileCSV.CriarArquivo(caminhoNulo, payments));
    }

    [Fact]
    public void CreateFile_ShouldWriteHeaderWhenPaymentsListIsEmpty()
    {
        //Arrange
        string caminho = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csv");
        var payments = new List<RoyaltyPayment>();
        try
        {
            //Act
            CreateFileCSV.CriarArquivo(caminho, payments);

            //Assert
            var linhas = File.ReadAllLines(caminho);
            Assert.Single(linhas);
            Assert.Equal("IdRecebedor;TipoRoyalty;TotalRoyalties", linhas[0]);
        }
        finally
        {
            if(File.Exists(caminho)) File.Delete(caminho);
        }
    }
}