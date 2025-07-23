using Application;
using Application.DTO;
using Domain.Interfaces;
using Domain.Models;

public interface IHolidayPlanService
{
    public Task<Result<HolidayPlanDTO>> AddHolidayPlan(Guid collaboratorId);
    public Task<Result<HolidayPeriodDTO>> AddHolidayPeriod(Guid collabId, CreateHolidayPeriodDTO holidayPeriodDTO);
    public Task SubmitHolidayPlanAsync(Guid id, Guid collabId, List<IHolidayPeriod> holidayPeriods);
    public Task SubmitHolidayPeriodAsync(Guid holidayPlanId, Guid id, PeriodDate periodDate);
    public Task<Result<HolidayPeriodDTO>> UpdateHolidayPeriodForCollaborator(Guid collabId, HolidayPeriodDTO periodDTO);

}

