using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ICollaboratorFactory
{
    Collaborator Create(ICollaboratorVisitor visitor);
}

