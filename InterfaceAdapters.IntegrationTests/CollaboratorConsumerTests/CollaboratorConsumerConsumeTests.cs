using Domain.Messages;
using Domain.Models;
using MassTransit;
using Moq;
using Xunit;

namespace InterfaceAdapters.IntegrationTests.CollaboratorConsumerTests;

public class CollaboratorConsumerConsumeTests
{
    [Fact]
    public async Task WhenMessageIsConsumed_ThenServiceMethodIsCalledWithData()
    {
        // Arrange
        var serviceMock = new Mock<ICollaboratorService>();
        var consumer = new CollaboratorConsumer(serviceMock.Object);

        var periodDateTime = new PeriodDateTime(DateTime.Now, DateTime.Now.AddDays(10));

        var message = new CollaboratorCreatedMessage(Guid.NewGuid(), Guid.NewGuid(), periodDateTime);

        var context = new Mock<ConsumeContext<CollaboratorCreatedMessage>>();
        context.Setup(c => c.Message).Returns(message);
        context.Setup(c => c.Headers.Get<string>("SenderId", null)).Returns("test-instance");

        await consumer.Consume(context.Object);

        // Assert
        serviceMock.Verify(s => s.SubmitCollaboratorAsync(message.Id, message.PeriodDateTime), Times.Once);
    }
}
