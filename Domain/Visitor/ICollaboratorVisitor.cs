using Domain.Models;

namespace Domain.Visitor;

public interface ICollaboratorVisitor
{
    Guid Id { get; }
    PeriodDateTime PeriodDateTime { get; }
}

