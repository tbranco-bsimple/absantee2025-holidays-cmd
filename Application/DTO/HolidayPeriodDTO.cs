using Domain.Models;

namespace Application.DTO;

public record HolidayPeriodDTO
{
    public Guid Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public HolidayPeriodDTO()
    {
    }

    public HolidayPeriodDTO(Guid id, PeriodDate periodDate)
    {
        Id = id;
        PeriodDate = periodDate;
    }
}
