using Domain.Interfaces;
using Domain.Models;

public interface IMessagePublisher
{
    Task PublishCreatedHolidayPlanMessageAsync(Guid id, Guid collaboratorId, List<HolidayPeriod> holidayPeriods);
    Task PublishUpdatedHolidayPeriodMessageAsync(Guid id, PeriodDate period);
    Task PublishCreatedHolidayPeriodMessageAsync(Guid holidayPlanId, Guid id, PeriodDate period);
}