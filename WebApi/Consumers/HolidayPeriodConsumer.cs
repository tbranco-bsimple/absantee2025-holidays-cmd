using Application.Services;
using WebApi.Messages;
using MassTransit;

public class HolidayPeriodConsumer : IConsumer<HolidayPeriodCreatedMessage>
{
    private readonly HolidayPlanService _holidayPlanService;

    public HolidayPeriodConsumer(HolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    public async Task Consume(ConsumeContext<HolidayPeriodCreatedMessage> context)
    {
        var senderId = context.Headers.Get<string>("SenderId");
        if (senderId == InstanceInfo.InstanceId)
            return;

        var msg = context.Message;
        await _holidayPlanService.SubmitHolidayPeriodAsync(msg.HolidayPlanId, msg.Id, msg.PeriodDate);
    }
}