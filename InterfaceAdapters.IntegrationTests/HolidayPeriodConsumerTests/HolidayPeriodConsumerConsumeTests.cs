using Domain.Messages;
using Domain.Models;
using MassTransit;
using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.HolidayPeriodConsumerTests;

public class HolidayPeriodConsumerConsumeTests
{
    [Fact]
    public async Task WhenMessageIsConsumed_ThenServiceMethodIsCalledWithData()
    {
        // Arrange
        var serviceMock = new Mock<IHolidayPlanService>();
        var consumer = new HolidayPeriodConsumer(serviceMock.Object);

        var periodDate = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(10));

        var message = new HolidayPeriodCreatedMessage(Guid.NewGuid(), Guid.NewGuid(), periodDate);

        var context = new Mock<ConsumeContext<HolidayPeriodCreatedMessage>>();
        context.Setup(c => c.Message).Returns(message);
        context.Setup(c => c.Headers.Get<string>("SenderId", null)).Returns("test-instance");

        await consumer.Consume(context.Object);

        // Assert
        serviceMock.Verify(s => s.SubmitHolidayPeriodAsync(message.HolidayPlanId, message.Id, message.PeriodDate), Times.Once);
    }
}
