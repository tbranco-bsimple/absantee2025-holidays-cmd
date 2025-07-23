using Application;
using Application.DTO;
using Domain.Interfaces;
using Domain.Models;

public interface IHolidayPlanService
{
    public Task<Result<HolidayPlanDTO>> AddHolidayPlan(Guid collaboratorId);
    public Task<Result<HolidayPeriodDTO>> AddHolidayPeriod(Guid collabId, CreateHolidayPeriodDTO holidayPeriodDTO);
    public Task AddConsumedHolidayPlan(Guid id, Guid collabId, List<HolidayPeriod> holidayPeriods);
    public Task AddConsumedHolidayPeriod(Guid holidayPlanId, Guid id, PeriodDate periodDate);
    public Task UpdateConsumedHolidayPeriod(Guid id, PeriodDate periodDate);
    public Task<Result<HolidayPeriodDTO>> UpdateHolidayPeriod(HolidayPeriodDTO periodDTO);

}

