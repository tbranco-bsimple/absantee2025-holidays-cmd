using Application.Services;
using Domain.Messages;
using MassTransit;

public class HolidayPeriodUpdatedConsumer : IConsumer<HolidayPeriodUpdatedMessage>
{
    private readonly IHolidayPlanService _holidayPlanService;

    public HolidayPeriodUpdatedConsumer(IHolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    public async Task Consume(ConsumeContext<HolidayPeriodUpdatedMessage> context)
    {
        var msg = context.Message;
        await _holidayPlanService.UpdateConsumedHolidayPeriod(msg.Id, msg.PeriodDate);
    }
}