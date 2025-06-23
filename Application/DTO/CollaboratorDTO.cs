
using Domain.Models;

namespace Application.DTO;

public record CollaboratorDTO
{
    public Guid Id { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

}
