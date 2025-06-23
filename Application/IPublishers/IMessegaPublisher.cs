using Domain.Interfaces;
using Domain.Models;

public interface IMessagePublisher
{
    Task PublishCreatedHolidayPlanMessageAsync(Guid id, Guid collabId, List<HolidayPeriod> holidayPeriods);
    Task PublishCreatedHolidayPeriodMessageAsync(Guid holidayPlanId, Guid id, PeriodDate period);
}