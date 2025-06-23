using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ICollaboratorFactory
{
    Collaborator Create(Guid id, PeriodDateTime periodDateTime);
    Collaborator Create(ICollaboratorVisitor visitor);
}

