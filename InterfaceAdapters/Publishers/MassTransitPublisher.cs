using Domain.Models;
using Domain.Messages;
using MassTransit;
using Domain.Interfaces;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishCreatedHolidayPeriodMessageAsync(Guid holidayPlanId, Guid id, PeriodDate period)
    {
        await _publishEndpoint.Publish(new HolidayPeriodCreatedMessage(holidayPlanId, id, period),
            context =>
            {
                context.Headers.Set("SenderId", InstanceInfo.InstanceId);
            });
    }

    public async Task PublishCreatedHolidayPlanMessageAsync(Guid id, Guid collaboratorId, List<IHolidayPeriod> holidayPeriods)
    {
        await _publishEndpoint.Publish(new HolidayPlanCreatedMessage(id, collaboratorId, holidayPeriods),
            context =>
            {
                context.Headers.Set("SenderId", InstanceInfo.InstanceId);
            });
    }
}