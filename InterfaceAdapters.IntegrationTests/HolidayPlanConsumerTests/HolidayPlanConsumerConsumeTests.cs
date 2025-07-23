using Domain.Interfaces;
using Domain.Messages;
using Domain.Models;
using MassTransit;
using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.HolidayPlanConsumerTests;

public class HolidayPlanConsumerConsumeTests
{
    [Fact]
    public async Task WhenMessageIsConsumed_ThenServiceMethodIsCalledWithData()
    {
        // Arrange
        var serviceMock = new Mock<IHolidayPlanService>();
        var consumer = new HolidayPlanConsumer(serviceMock.Object);

        var holidayPeriods = new List<IHolidayPeriod> { Mock.Of<IHolidayPeriod>() };

        var message = new HolidayPlanCreatedMessage(Guid.NewGuid(), Guid.NewGuid(), holidayPeriods);

        var context = new Mock<ConsumeContext<HolidayPlanCreatedMessage>>();
        context.Setup(c => c.Message).Returns(message);
        context.Setup(c => c.Headers.Get<string>("SenderId", null)).Returns("test-instance");

        await consumer.Consume(context.Object);

        // Assert
        serviceMock.Verify(s => s.SubmitHolidayPlanAsync(message.Id, message.CollaboratorId, message.HolidayPeriods), Times.Once);
    }
}
