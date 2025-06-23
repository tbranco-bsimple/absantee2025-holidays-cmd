using Domain.Models;

namespace WebApi.Messages;

public record HolidayPeriodCreatedMessage(Guid HolidayPlanId, Guid Id, PeriodDate PeriodDate);