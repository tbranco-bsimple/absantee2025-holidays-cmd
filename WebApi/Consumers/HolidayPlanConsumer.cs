using Application.Services;
using WebApi.Messages;
using MassTransit;

public class HolidayPlanConsumer : IConsumer<HolidayPlanCreatedMessage>
{
    private readonly HolidayPlanService _holidayPlanService;

    public HolidayPlanConsumer(HolidayPlanService holidayPlanService)
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