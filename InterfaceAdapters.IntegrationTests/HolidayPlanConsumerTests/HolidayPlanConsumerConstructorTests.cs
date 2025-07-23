using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.HolidayPlanConsumerTests;

public class HolidayPlanConsumerConstructorTests
{
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange
        var serviceMock = new Mock<IHolidayPlanService>();

        // Act
        new HolidayPlanConsumer(serviceMock.Object);

        // Assert
    }
}
