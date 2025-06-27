using Domain.Models;

namespace Domain.Messages;

public record HolidayPeriodCreatedMessage(Guid HolidayPlanId, Guid Id, PeriodDate PeriodDate);