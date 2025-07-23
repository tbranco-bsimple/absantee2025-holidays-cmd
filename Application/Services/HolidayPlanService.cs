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
            var holidayPlan = await _holidayPlanFactory.Create(collaborator, []);
            await _holidayPlanRepository.AddHolidayPlanAsync(holidayPlan);

            await _messagePublisher.PublishCreatedHolidayPlanMessageAsync(holidayPlan.Id, holidayPlan.CollaboratorId, holidayPlan.HolidayPeriods);

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

    public async Task SubmitHolidayPlanAsync(Guid id, Guid collabId, List<IHolidayPeriod> holidayPeriods)
    {
        var holidayPlan = await _holidayPlanRepository.GetByIdAsync(id);

        if (holidayPlan != null)
        {
            Console.WriteLine($"HolidayPlanConsumed not added, already exists with Id: {id}");
            return;
        }

        var holidayPeriodsDataModel = _mapper.Map<List<IHolidayPeriod>, List<HolidayPeriodDataModel>>(holidayPeriods);
        var visitor = new HolidayPlanDataModel()
        {
            Id = id,
            CollaboratorId = collabId,
            HolidayPeriods = holidayPeriodsDataModel
        };

        holidayPlan = _holidayPlanFactory.Create(visitor);

        await _holidayPlanRepository.AddAsync(holidayPlan);
    }

    public async Task SubmitHolidayPeriodAsync(Guid holidayPlanId, Guid id, PeriodDate periodDate)
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

    public async Task<Result<HolidayPeriodDTO>> UpdateHolidayPeriodForCollaborator(Guid collabId, HolidayPeriodDTO periodDTO)
    {
        try
        {
            var period = _mapper.Map<HolidayPeriodDTO, HolidayPeriod>(periodDTO);
            var updatedResult = await _holidayPlanRepository.UpdateHolidayPeriodAsync(collabId, period);

            var result = new HolidayPeriodDTO(updatedResult.Id, updatedResult.PeriodDate);

            return Result<HolidayPeriodDTO>.Success(result);
        }
        catch (Exception e)
        {
            return Result<HolidayPeriodDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }
}
