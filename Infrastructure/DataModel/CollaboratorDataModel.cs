using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("Collaborators")]
public class CollaboratorDataModel : ICollaboratorVisitor
{
    public Guid Id { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public CollaboratorDataModel(ICollaborator collaborator)
    {
        Id = collaborator.Id;
        PeriodDateTime = collaborator.PeriodDateTime;
    }

    public CollaboratorDataModel()
    {
    }
}