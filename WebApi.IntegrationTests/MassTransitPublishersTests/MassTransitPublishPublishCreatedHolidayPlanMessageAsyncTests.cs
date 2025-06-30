using Domain.Interfaces;
using Domain.Messages;
using Domain.Models;
using MassTransit;
using Moq;
using Xunit;

namespace WebApi.IntegrationTests.MassTransitPublisherTests;

public class MassTransitPublishPublishCreatedHolidayPlanMessageAsyncTests
{

    [Fact]
    public async Task WhenPublisherIsCalled_ThenPublishHolidayPlan()
    {
        // Arrange
        var endpointMock = new Mock<IPublishEndpoint>();
        var publisher = new MassTransitPublisher(endpointMock.Object);

        var id = Guid.NewGuid();
        var collaboratorId = Guid.NewGuid();
        var holidayPeriods = new List<IHolidayPeriod> { Mock.Of<IHolidayPeriod>() };

        // Act
        await publisher.PublishCreatedHolidayPlanMessageAsync(id, collaboratorId, holidayPeriods);

        // Assert
        endpointMock.Verify(
            p => p.Publish(
                It.Is<HolidayPlanCreatedMessage>(m =>
                    m.Id == id &&
                    m.CollaboratorId == collaboratorId
                ),
                It.IsAny<IPipe<PublishContext<HolidayPlanCreatedMessage>>>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }
}
