using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IHolidayPlanRepository : IGenericRepositoryEF<IHolidayPlan, HolidayPlan, IHolidayPeriodVisitor>
{
    public Task<bool> CanInsertHolidayPlan(Guid collaboratorId);
    public Task<bool> CanInsertHolidayPeriod(Guid holidayPlanId, HolidayPeriod periodDate);
    public Task<HolidayPlan> AddHolidayPlanAsync(HolidayPlan holidayPlan);
    public Task<HolidayPeriod> AddHolidayPeriodAsync(Guid holidayPlanId, HolidayPeriod holidayPeriod);
    public Task<HolidayPeriod> UpdateHolidayPeriodAsync(Guid holidayPeriodId, HolidayPeriod holidayPeriod);
    public Task<HolidayPlan?> GetHolidayPlanByCollaboratorAsync(Guid collabId);
    public Task<HolidayPeriod?> GetHolidayPeriodByIdAsync(Guid holidayPeriodId);
}
