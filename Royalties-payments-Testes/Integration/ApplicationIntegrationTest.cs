using System.Diagnostics;

namespace Royalties_payments_Testes.Integration;
public class ApplicationIntegrationTest
{
    [Fact]
    public void Execute_ShouldCompleteSuccessfully()
    {
        //Arrange
        string repositoryDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string projectDirectory = Path.Combine(repositoryDirectory, "Royalties-payments-Testes");
        string jsonDirectory = Path.Combine(projectDirectory, "bin", "Debug", "net10.0", "Data");
        string trackPath = Path.Combine(jsonDirectory, "tracks-metadata.json");
        string subscribersPath = Path.Combine(jsonDirectory, "subscribers-streams.json");

        string[] args = { trackPath, subscribersPath };
        //Act
        Program.Main(args);
        string csvPath = Path.Combine(projectDirectory, "bin", "Debug", "net10.0", "Royalties-payment.csv");

        //Assert
        Assert.True(File.Exists(csvPath));
    }

    [Fact]
    public void Execute_ShouldProcessJsonFilesCorrectly()
    {
        //Arrange
        string repositoryDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string projectDirectory = Path.Combine(repositoryDirectory, "Royalties-payments-Testes");
        string jsonDirectory = Path.Combine(projectDirectory, "bin", "Debug", "net10.0", "Data");

        string applicationPath = Path.Combine(projectDirectory, "bin", "Debug", "net10.0", "Royalties-payments.exe");
        string trackPath = Path.Combine(jsonDirectory, "tracks-metadata.json");
        string subscribersPath = Path.Combine(jsonDirectory, "subscribers-streams.json");
        string csvPath = Path.Combine(projectDirectory, "bin", "Debug", "net10.0", "Royalties-payment.csv");

        var process = new Process();
        process.StartInfo.FileName = applicationPath;
        process.StartInfo.Arguments = $"\"{trackPath}\" \"{subscribersPath}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;

        //Act
        process.Start();
        string csvContent = File.ReadAllText(csvPath);
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();
        //Assert
        Assert.Equal(0, process.ExitCode);
        Assert.True(File.Exists(csvPath));
        Assert.Contains("IdRecebedor;TipoRoyalty;TotalRoyalties", csvContent);
        Assert.Empty(error);
    }
}