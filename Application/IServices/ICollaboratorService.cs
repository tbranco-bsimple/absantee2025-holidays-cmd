using Domain.Models;

public interface ICollaboratorService
{
    public Task SubmitCollaboratorAsync(Guid id, PeriodDateTime periodDateTime);
}

