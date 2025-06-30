using Application.Services;
using Domain.Messages;
using MassTransit;

public class CollaboratorConsumer : IConsumer<CollaboratorCreatedMessage>
{
    private readonly ICollaboratorService _collaboratorService;

    public CollaboratorConsumer(ICollaboratorService collaboratorService)
    {
        _collaboratorService = collaboratorService;
    }

    public async Task Consume(ConsumeContext<CollaboratorCreatedMessage> context)
    {
        var senderId = context.Headers.Get<string>("SenderId");
        if (senderId == InstanceInfo.InstanceId)
            return;

        var msg = context.Message;
        await _collaboratorService.SubmitCollaboratorAsync(msg.Id, msg.PeriodDateTime);
    }
}