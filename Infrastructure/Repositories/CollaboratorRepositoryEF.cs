using Domain.Models;
using Infrastructure.DataModel;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CollaboratorRepositoryEF : GenericRepositoryEF<ICollaborator, Collaborator, CollaboratorDataModel>, ICollaboratorRepository
{
    private IMapper _mapper;

    public CollaboratorRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<Collaborator> AddCollaboratorAsync(Collaborator collaborator)
    {
        var collabExists = await _context.Set<CollaboratorDataModel>()
            .FirstOrDefaultAsync(c => c.Id == collaborator.Id);

        if (collabExists != null)
            throw new Exception("Collaborator already exists.");

        var collabDataModel = _mapper.Map<Collaborator, CollaboratorDataModel>(collaborator);
        await _context.Set<CollaboratorDataModel>().AddAsync(collabDataModel);

        return collaborator;
    }

    public async Task<Collaborator> UpdateCollaboratorAsync(Collaborator collaborator)
    {
        var collaboratorDM = await _context.Set<CollaboratorDataModel>()
           .FirstOrDefaultAsync(c => c.Id == collaborator.Id);

        if (collaboratorDM == null)
            return null;

        _context.Set<CollaboratorDataModel>().Update(collaboratorDM);

        var collab = _mapper.Map<CollaboratorDataModel, Collaborator>(collaboratorDM);
        return collab;
    }

    public override ICollaborator? GetById(Guid id)
    {
        var collabDM = _context.Set<CollaboratorDataModel>().FirstOrDefault(c => c.Id == id);

        if (collabDM == null)
            return null;

        var collab = _mapper.Map<CollaboratorDataModel, Collaborator>(collabDM);
        return collab;
    }

    public override async Task<ICollaborator?> GetByIdAsync(Guid id)
    {
        var collabDM = await _context.Set<CollaboratorDataModel>().FirstOrDefaultAsync(c => c.Id == id);

        if (collabDM == null)
            return null;

        var collab = _mapper.Map<CollaboratorDataModel, Collaborator>(collabDM);
        return collab;
    }
}