using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/holidays")]
[ApiController]
public class HolidayPlanController : ControllerBase
{
    private readonly IHolidayPlanService _holidayPlanService;

    public HolidayPlanController(IHolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    [HttpPost("{collaboratorId}/holidayplan")]
    public async Task<ActionResult<HolidayPlanDTO>> AddHolidayPlanForCollaborator(Guid collaboratorId)
    {
        var result = await _holidayPlanService.AddHolidayPlan(collaboratorId);

        return result.ToActionResult();
    }

    [HttpPost("{collaboratorId}/holidayperiod")]
    public async Task<ActionResult<HolidayPeriodDTO>> AddHolidayPeriodForCollaborator(Guid collaboratorId, [FromBody] CreateHolidayPeriodDTO hp)
    {
        var result = await _holidayPlanService.AddHolidayPeriod(collaboratorId, hp);

        return result.ToActionResult();
    }

    [HttpPut("{collaboratorId}/holidayPeriod")]
    public async Task<ActionResult<HolidayPeriodDTO>> UpdateHolidayPeriodsOfCollaborator([FromBody] HolidayPeriodDTO hp)
    {
        var result = await _holidayPlanService.UpdateHolidayPeriod(hp);

        return result.ToActionResult();
    }

}