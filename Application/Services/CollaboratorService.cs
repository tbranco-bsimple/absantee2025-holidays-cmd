using Domain.IRepository;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Application.Services;

public class CollaboratorService : ICollaboratorService
{
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly ICollaboratorFactory _collaboratorFactory;

    public CollaboratorService(ICollaboratorRepository collaboratorRepository, ICollaboratorFactory collaboratorFactory, IMapper mapper)
    {
        _collaboratorRepository = collaboratorRepository;
        _collaboratorFactory = collaboratorFactory;
    }

    public async Task SubmitCollaboratorAsync(Guid id, PeriodDateTime periodDateTime)
    {
        var collaborator = await _collaboratorRepository.GetByIdAsync(id);

        if (collaborator != null)
        {
            Console.WriteLine($"CollaboratorConsumed not added, already exists with Id: {id}");
            return;
        }

        var visitor = new CollaboratorDataModel()
        {
            Id = id,
            PeriodDateTime = periodDateTime
        };

        collaborator = _collaboratorFactory.Create(visitor);

        await _collaboratorRepository.AddAsync(collaborator);
    }

}
