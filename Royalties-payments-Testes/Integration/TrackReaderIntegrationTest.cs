using Royalties_payments.Reader;
using System.Text.Json;

namespace Royalties_payments_Testes.Integration;
public class TrackReaderIntegrationTest
{
    [Fact]
    public void Read_ShouldReturnTrackData_WhenFileIsValid()
    {
        // Arrange
        string filePath = "Data/tracks-metadata.json";
        // Act
        var trackData = TrackReader.Read(filePath);
        // Assert
        Assert.NotNull(trackData);
        Assert.Equal("August", trackData.Tracks[0].Title);
        Assert.Equal("Taylor Swift", trackData.Performers[0].Name);
        Assert.Equal(2020, trackData.Tracks[0].ReleaseDate.Year);
    }

    [Fact]
    public void Read_ShouldThrowDirectoryNotFoundException_WhenDirectoryIsInvalid()
    {
        // Arrange
        string filePath = "TestData/tracks-metadata.json";
        // Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => TrackReader.Read(filePath));
    }

    [Fact]
    public void Read_ShouldThrowInvalidOperationException_WhenFileIsEmpty()
    {
        // Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, "");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => TrackReader.Read(filePath));

        File.Delete(filePath);
    }

    [Fact]
    public void Read_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
    {
        // Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, "{ json inválido }");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => TrackReader.Read(filePath));

        File.Delete(filePath);
    }

    [Fact]
    public void Read_ShouldThrowInvalidOperationException_WithJsonExceptionAsInnerException()
    {
        // Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, "{ json inválido }");
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => TrackReader.Read(filePath));
        Assert.IsType<JsonException>(exception.InnerException);
        File.Delete(filePath);
    }
}