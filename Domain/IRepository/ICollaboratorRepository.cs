using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepositoryEF<ICollaborator, Collaborator, ICollaboratorVisitor>
{
    public Task<Collaborator> AddCollaboratorAsync(Collaborator collaborator);
    public Task<Collaborator> UpdateCollaboratorAsync(Collaborator collaborator);
}
