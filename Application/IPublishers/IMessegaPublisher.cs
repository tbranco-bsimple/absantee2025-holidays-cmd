using Domain.Interfaces;
using Domain.Models;

public interface IMessagePublisher
{
    Task PublishCreatedHolidayPlanMessageAsync(Guid id, Guid collaboratorId, List<IHolidayPeriod> holidayPeriods);
    Task PublishCreatedHolidayPeriodMessageAsync(Guid holidayPlanId, Guid id, PeriodDate period);
}