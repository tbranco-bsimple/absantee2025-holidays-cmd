using Application.DTO;
using Domain.Interfaces;
using Domain.Models;

namespace WebApi.Messages;

public record HolidayPlanCreatedMessage(Guid Id, Guid CollaboratorId, List<HolidayPeriod> HolidayPeriods);