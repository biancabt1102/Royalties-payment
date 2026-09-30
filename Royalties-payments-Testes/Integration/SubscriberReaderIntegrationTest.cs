using Royalties_payments.Models;
using Royalties_payments.Reader;
using System.Text.Json;

namespace Royalties_payments_Testes.Integration;
public class SubscriberReaderIntegrationTest
{
    [Fact]
    public void Read_ShouldReturnSubscriberList_WhenFileIsValid()
    {
        //Arrange
        string filePath = "Data/subscribers-streams.json";
        //Act
        List<Subscriber> subscribers = SubscriberReader.Read(filePath);
        //Assert
        Assert.NotNull(subscribers);
        Assert.Equal(25, subscribers[0].TotalPaidSubscription);
    }

    [Fact]
    public void Read_ShouldThrowException_WhenDirectoryIsInvalid()
    {
        //Arrange
        string filePath = "Data/invalid-directory/subscribers-streams.json";
        //Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => SubscriberReader.Read(filePath));
    }

    [Fact]
    public void Read_ShouldThrowinvalidOperationException_WhenFileIsEmpty()
    {
        //Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, "");
        //Act & Assert
        Assert.Throws<InvalidOperationException>(() => SubscriberReader.Read(filePath));
        File.Delete(filePath);
    }

    [Fact]
    public void Read_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
    {
        //Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, "{ json inválido }");
        //Act & Assert
        Assert.Throws<InvalidOperationException>(() => SubscriberReader.Read(filePath));
        File.Delete(filePath);
    }

    [Fact]
    public void Read_ShouldThrowInvalidOperationException_WithJsonExceptionAsInnerException()
    {
        //Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, "{ json inválido }");
        //Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => SubscriberReader.Read(filePath));
        Assert.IsType<JsonException>(exception.InnerException);
    }
}