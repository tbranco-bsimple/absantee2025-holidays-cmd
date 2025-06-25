using Domain.IRepository;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Application.Services;

public class CollaboratorService
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
        var visitor = new CollaboratorDataModel()
        {
            Id = id,
            PeriodDateTime = periodDateTime
        };

        var collaborator = _collaboratorFactory.Create(visitor);

        await _collaboratorRepository.AddAsync(collaborator);
    }

}
