using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.CollaboratorConsumerTests;

public class CollaboratorConsumerConstructorTests
{
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange
        var serviceMock = new Mock<ICollaboratorService>();

        // Act
        new CollaboratorConsumer(serviceMock.Object);

        // Assert
    }
}
