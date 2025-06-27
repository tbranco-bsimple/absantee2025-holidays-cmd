using Domain.Models;

namespace Domain.Messages;

public record HolidayPlanCreatedMessage(Guid Id, Guid CollaboratorId, List<HolidayPeriod> HolidayPeriods);