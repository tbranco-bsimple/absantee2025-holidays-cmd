using Application.Services;
using Domain.Messages;
using MassTransit;

public class HolidayPlanCreatedConsumer : IConsumer<HolidayPlanCreatedMessage>
{
    private readonly IHolidayPlanService _holidayPlanService;

    public HolidayPlanCreatedConsumer(IHolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    public async Task Consume(ConsumeContext<HolidayPlanCreatedMessage> context)
    {
        var msg = context.Message;
        await _holidayPlanService.AddConsumedHolidayPlan(msg.Id, msg.CollaboratorId, msg.HolidayPeriods);
    }
}