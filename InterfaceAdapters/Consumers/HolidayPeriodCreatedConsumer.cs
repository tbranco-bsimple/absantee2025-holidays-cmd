using Domain.Messages;
using MassTransit;

public class HolidayPeriodCreatedConsumer : IConsumer<HolidayPeriodCreatedMessage>
{
    private readonly IHolidayPlanService _holidayPlanService;

    public HolidayPeriodCreatedConsumer(IHolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    public async Task Consume(ConsumeContext<HolidayPeriodCreatedMessage> context)
    {
        var msg = context.Message;
        await _holidayPlanService.AddConsumedHolidayPeriod(msg.HolidayPlanId, msg.Id, msg.PeriodDate);
    }
}