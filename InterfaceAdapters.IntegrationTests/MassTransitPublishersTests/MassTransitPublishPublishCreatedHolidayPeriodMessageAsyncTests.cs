using Domain.Messages;
using Domain.Models;
using MassTransit;
using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.MassTransitPublisherTests;

public class MassTransitPublishPublishCreatedHolidayPeriodMessageAsyncTests
{
    [Fact]
    public async Task WhenPublisherIsCalled_ThenPublishHolidayPeriod()
    {
        // Arrange
        var endpointMock = new Mock<IPublishEndpoint>();
        var publisher = new MassTransitPublisher(endpointMock.Object);

        var holidayPlanId = Guid.NewGuid();
        var id = Guid.NewGuid();
        var periodDate = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(5));

        // Act
        await publisher.PublishCreatedHolidayPeriodMessageAsync(holidayPlanId, id, periodDate);

        // Assert
        endpointMock.Verify(
            p => p.Publish(
                It.Is<HolidayPeriodCreatedMessage>(m =>
                    m.HolidayPlanId == holidayPlanId &&
                    m.Id == id &&
                    m.PeriodDate == periodDate
                ),
                It.IsAny<IPipe<PublishContext<HolidayPeriodCreatedMessage>>>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }
}
