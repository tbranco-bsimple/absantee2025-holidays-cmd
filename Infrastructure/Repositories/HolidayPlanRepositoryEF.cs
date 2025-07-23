using Domain.Models;
using Infrastructure.DataModel;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class HolidayPlanRepositoryEF : GenericRepositoryEF<IHolidayPlan, HolidayPlan, HolidayPlanDataModel>, IHolidayPlanRepository
{
    private IMapper _mapper;

    public HolidayPlanRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> CanInsertHolidayPlan(Guid collaboratorId)
    {
        return !await _context.Set<HolidayPlanDataModel>().AnyAsync(hp => hp.CollaboratorId == collaboratorId);
    }

    public async Task<bool> CanInsertHolidayPeriod(Guid holidayPlanId, HolidayPeriod periodDate)
    {
        return !await _context.Set<HolidayPlanDataModel>().AnyAsync
            (h => h.Id == holidayPlanId && h.HolidayPeriods.Any
                (hp => hp.PeriodDate.InitDate <= periodDate.PeriodDate.InitDate
                    && hp.PeriodDate.FinalDate >= periodDate.PeriodDate.FinalDate));
    }

    public async Task<HolidayPlan> AddHolidayPlanAsync(HolidayPlan holidayPlan)
    {
        var holidayPlanExists = await _context.Set<HolidayPlanDataModel>()
            .Include(h => h.HolidayPeriods)
            .FirstOrDefaultAsync(h => h.Id == holidayPlan.Id);

        if (holidayPlanExists != null)
            throw new Exception("Holiday Plan already exists");

        var dataModel = _mapper.Map<HolidayPlan, HolidayPlanDataModel>(holidayPlan);

        Console.WriteLine("REPO id é :", dataModel.Id);

        await _context.Set<HolidayPlanDataModel>().AddAsync(dataModel);
        await SaveChangesAsync();

        return _mapper.Map<HolidayPlanDataModel, HolidayPlan>(dataModel);
    }

    public async Task<HolidayPeriod> AddHolidayPeriodAsync(Guid holidayPlanId, HolidayPeriod holidayPeriod)
    {
        var holidayPlan = await _context.Set<HolidayPlanDataModel>()
            .Include(h => h.HolidayPeriods)
            .FirstOrDefaultAsync(h => h.Id == holidayPlanId);

        if (holidayPlan == null)
            throw new Exception("Holiday Plan doesn't exist");

        var dataModel = _mapper.Map<HolidayPeriod, HolidayPeriodDataModel>(holidayPeriod);

        holidayPlan.HolidayPeriods.Add(dataModel);

        await SaveChangesAsync();

        return _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(dataModel);
    }

    public async Task<HolidayPeriod> UpdateHolidayPeriodAsync(Guid collabId, HolidayPeriod holidayPeriod)
    {
        var holidayPlan = await _context.Set<HolidayPlanDataModel>()
            .Include(h => h.HolidayPeriods)
            .FirstOrDefaultAsync(h => h.CollaboratorId == collabId);

        if (holidayPlan == null)
            throw new KeyNotFoundException("HolidayPlan not found");

        var period = holidayPlan.HolidayPeriods
            .FirstOrDefault(p => p.Id == holidayPeriod.Id);

        if (period == null)
            throw new KeyNotFoundException("HolidayPeriod not found");

        _mapper.Map(holidayPeriod, period);
        await SaveChangesAsync();

        return _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(period);
    }

    public override async Task<IHolidayPlan?> GetByIdAsync(Guid id)
    {
        var hpDM = await _context.Set<HolidayPlanDataModel>().FirstOrDefaultAsync(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDM);
        return hp;
    }

    public override IHolidayPlan? GetById(Guid id)
    {
        var hpDM = _context.Set<HolidayPlanDataModel>().FirstOrDefault(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDM);
        return hp;
    }

    public async Task<HolidayPlan?> GetHolidayPlanByCollaboratorAsync(Guid collaboratorId)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriods)
            .SingleOrDefaultAsync();

        if (hpDm == null) return null;

        return _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDm);
    }

    public async Task<HolidayPeriod?> GetHolidayPeriodByIdAsync(Guid holidayPeriodId)
    {
        var hpDM = await _context.Set<HolidayPeriodDataModel>().FirstOrDefaultAsync(hp => hp.Id == holidayPeriodId);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(hpDM);
        return hp;
    }
}