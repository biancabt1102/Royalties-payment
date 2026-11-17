using Royalties_payments.Models;
using Royalties_payments_Testes.Builder;
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
        List<Subscriber> subscribers = SubscribersBuilder.Create(filePath);
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
        Assert.Throws<DirectoryNotFoundException>(() => SubscribersBuilder.Create(filePath));
    }

    [Fact]
    public void Read_ShouldThrowinvalidOperationException_WhenFileIsEmpty()
    {
        //Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        try
        {
            File.WriteAllText(filePath, "");
            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => SubscribersBuilder.Create(filePath));
        }
        finally
        {
            if(File.Exists(filePath)) File.Delete(filePath);
        }
    }

    [Fact]
    public void Read_ShouldThrowJsonException_WhenJsonIsInvalid()
    {
        //Arrange
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        try
        {
            File.WriteAllText(filePath, "{ json inválido }");
            //Act & Assert
            Assert.Throws<JsonException>(() => SubscribersBuilder.Create(filePath));
        }
        finally
        {
            if(File.Exists(filePath)) File.Delete(filePath);
        }
    }
}