using Domain.Models;

namespace Domain.Interfaces;

public interface ICollaborator
{
    Guid Id { get; }
    PeriodDateTime PeriodDateTime { get; }
    public bool ContractContainsDates(PeriodDateTime periodDateTime);
}
