using Moq;
using Xunit;

namespace WebApi.IntegrationTests.HolidayPeriodConsumerTests;

public class HolidayPeriodConsumerConstructorTests
{
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange
        var serviceMock = new Mock<IHolidayPlanService>();

        // Act
        new HolidayPeriodConsumer(serviceMock.Object);

        // Assert
    }
}
