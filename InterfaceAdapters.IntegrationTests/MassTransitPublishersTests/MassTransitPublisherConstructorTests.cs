using MassTransit;
using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.MassTransitPublisherTests;

public class MassTransitPublisherConstructorTests
{
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange
        var endpointMock = new Mock<IPublishEndpoint>();

        // Act
        new MassTransitPublisher(endpointMock.Object);

        // Assert
    }
}
