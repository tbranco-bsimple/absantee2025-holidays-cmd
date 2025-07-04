using Application.Services;
using Domain.Messages;
using MassTransit;

public class HolidayPlanConsumer : IConsumer<HolidayPlanCreatedMessage>
{
    private readonly IHolidayPlanService _holidayPlanService;

    public HolidayPlanConsumer(IHolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    public async Task Consume(ConsumeContext<HolidayPlanCreatedMessage> context)
    {
        var senderId = context.Headers.Get<string>("SenderId");
        if (senderId == InstanceInfo.InstanceId)
            return;

        var msg = context.Message;
        await _holidayPlanService.SubmitHolidayPlanAsync(msg.Id, msg.CollaboratorId, msg.HolidayPeriods);
    }
}