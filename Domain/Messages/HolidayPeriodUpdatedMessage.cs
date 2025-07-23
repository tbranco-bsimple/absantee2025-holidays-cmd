using Domain.Models;

namespace Domain.Messages;

public record HolidayPeriodUpdatedMessage(Guid Id, PeriodDate PeriodDate);