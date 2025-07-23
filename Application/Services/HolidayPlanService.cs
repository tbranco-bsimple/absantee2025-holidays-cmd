using Domain.IRepository;
using Domain.Models;
using Application.DTO;
using AutoMapper;
using Domain.Factory;
using Infrastructure.DataModel;
using Domain.Interfaces;

namespace Application.Services;

public class HolidayPlanService : IHolidayPlanService
{
    private readonly IHolidayPlanRepository _holidayPlanRepository;
    private readonly IHolidayPlanFactory _holidayPlanFactory;
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IMapper _mapper;

    public HolidayPlanService(IHolidayPlanRepository holidayPlanRepository, IHolidayPlanFactory holidayPlanFactory, IHolidayPeriodFactory holidayPeriodFactory, ICollaboratorRepository collaboratorRepository, IMessagePublisher messagePublisher, IMapper mapper)
    {
        _holidayPlanRepository = holidayPlanRepository;
        _holidayPlanFactory = holidayPlanFactory;
        _holidayPeriodFactory = holidayPeriodFactory;
        _collaboratorRepository = collaboratorRepository;
        _messagePublisher = messagePublisher;
        _mapper = mapper;
    }

    public async Task<Result<HolidayPlanDTO>> AddHolidayPlan(Guid collaboratorId)
    {
        try
        {
            var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorId);
            var holidayPlan = await _holidayPlanFactory.Create(collaborator!, []);
            await _holidayPlanRepository.AddHolidayPlanAsync(holidayPlan);

            var holidayPeriods = holidayPlan.HolidayPeriods
                .OfType<HolidayPeriod>()
                .ToList();

            await _messagePublisher.PublishCreatedHolidayPlanMessageAsync(holidayPlan.Id, holidayPlan.CollaboratorId, holidayPeriods);

            var result = _mapper.Map<HolidayPlan, HolidayPlanDTO>(holidayPlan);
            return Result<HolidayPlanDTO>.Success(result);
        }
        catch (Exception e)
        {
            return Result<HolidayPlanDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }

    // UC1
    public async Task<Result<HolidayPeriodDTO>> AddHolidayPeriod(Guid collabId, CreateHolidayPeriodDTO holidayPeriodDTO)
    {
        HolidayPeriod holidayPeriod;
        try
        {
            var holidayPlan = await _holidayPlanRepository.GetHolidayPlanByCollaboratorAsync(collabId);
            if (holidayPlan == null)
            {
                var holidayPlanCreated = await AddHolidayPlan(collabId);
                holidayPlan = _mapper.Map<HolidayPlanDTO, HolidayPlan>(holidayPlanCreated.Value);
            }

            holidayPeriod = await _holidayPeriodFactory.Create(holidayPlan!.Id, holidayPeriodDTO.InitDate, holidayPeriodDTO.FinalDate);
            await _holidayPlanRepository.AddHolidayPeriodAsync(holidayPlan.Id, holidayPeriod);

            await _messagePublisher.PublishCreatedHolidayPeriodMessageAsync(holidayPlan.Id, holidayPeriod.Id, holidayPeriod.PeriodDate);

            var result = _mapper.Map<HolidayPeriod, HolidayPeriodDTO>(holidayPeriod);
            return Result<HolidayPeriodDTO>.Success(result);
        }
        catch (Exception e)
        {
            return Result<HolidayPeriodDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task AddConsumedHolidayPlan(Guid id, Guid collabId, List<HolidayPeriod> holidayPeriods)
    {
        var holidayPlan = await _holidayPlanRepository.GetByIdAsync(id);

        if (holidayPlan != null)
        {
            Console.WriteLine($"HolidayPlanConsumed not added, already exists with Id: {id}");
            return;
        }

        var holidayPeriodsDataModel = _mapper.Map<List<HolidayPeriod>, List<HolidayPeriodDataModel>>(holidayPeriods);
        var visitor = new HolidayPlanDataModel()
        {
            Id = id,
            CollaboratorId = collabId,
            HolidayPeriods = holidayPeriodsDataModel
        };

        holidayPlan = _holidayPlanFactory.Create(visitor);

        await _holidayPlanRepository.AddAsync(holidayPlan);
    }

    public async Task AddConsumedHolidayPeriod(Guid holidayPlanId, Guid id, PeriodDate periodDate)
    {
        var holidayPeriod = await _holidayPlanRepository.GetHolidayPeriodByIdAsync(id);

        if (holidayPeriod != null)
        {
            Console.WriteLine($"HolidayPeriodConsumed not added, already exists with Id: {id}");
            return;
        }

        var visitor = new HolidayPeriodDataModel()
        {
            Id = id,
            PeriodDate = periodDate
        };

        holidayPeriod = _holidayPeriodFactory.Create(visitor);

        await _holidayPlanRepository.AddHolidayPeriodAsync(holidayPlanId, holidayPeriod);
    }

    public async Task UpdateConsumedHolidayPeriod(Guid id, PeriodDate periodDate)
    {
        var holidayPeriod = await _holidayPlanRepository.GetHolidayPeriodByIdAsync(id);

        if (holidayPeriod == null)
        {
            Console.WriteLine($"HolidayPeriodConsumed not updated, does not exist with Id: {id}");
            return;
        }

        var period = new HolidayPeriod(id, periodDate);

        await _holidayPlanRepository.UpdateHolidayPeriodAsync(id, period);
    }

    public async Task<Result<HolidayPeriodDTO>> UpdateHolidayPeriod(HolidayPeriodDTO periodDTO)
    {
        try
        {
            var period = _mapper.Map<HolidayPeriodDTO, HolidayPeriod>(periodDTO);
            var updatedResult = await _holidayPlanRepository.UpdateHolidayPeriodAsync(periodDTO.Id, period);

            var result = new HolidayPeriodDTO(updatedResult.Id, updatedResult.PeriodDate);

            await _messagePublisher.PublishUpdatedHolidayPeriodMessageAsync(result.Id, result.PeriodDate);

            return Result<HolidayPeriodDTO>.Success(result);
        }
        catch (Exception e)
        {
            return Result<HolidayPeriodDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }
}
