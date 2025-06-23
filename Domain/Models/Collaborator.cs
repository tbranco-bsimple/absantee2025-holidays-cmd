using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    public Guid Id { get; }
    public PeriodDateTime PeriodDateTime { get; }

    public Collaborator(Guid id, PeriodDateTime periodDateTime)
    {
        Id = id;
        PeriodDateTime = periodDateTime;
    }

    public bool ContractContainsDates(PeriodDateTime periodDateTime)
    {
        return PeriodDateTime.Contains(periodDateTime);
    }

}