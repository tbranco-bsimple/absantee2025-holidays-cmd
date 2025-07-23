using Application.Services;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Moq;

namespace Application.Tests.CollaboratorServiceTests;

public class CollaboratorServiceServiceAddConsumedTests
{
    [Fact]
    public async Task AddConsumed_WithValidData_ShouldAdd()
    {
        // Arrange
        var collaboratorRepository = new Mock<ICollaboratorRepository>();
        var collaboratorFactory = new Mock<ICollaboratorFactory>();
        var mapper = new Mock<IMapper>();

        var id = Guid.NewGuid();
        var period = new PeriodDateTime(DateTime.Now, DateTime.Now.AddDays(5));



        collaboratorRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Collaborator)null);

        var expectedCollaborator = new Mock<Collaborator>();
        collaboratorFactory.Setup(f => f.Create(It.IsAny<CollaboratorDataModel>()))
               .Returns(expectedCollaborator.Object);


        var service = new CollaboratorService(collaboratorRepository.Object, collaboratorFactory.Object, mapper.Object);

        // Act
        await service.SubmitCollaboratorAsync(id, period);

        // Assert
        collaboratorRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        collaboratorFactory.Verify(r => r.Create(It.IsAny<CollaboratorDataModel>()), Times.Once);
        collaboratorRepository.Verify(r => r.AddAsync(expectedCollaborator.Object), Times.Once);
    }

    [Fact]
    public async Task AddConsumed_WithAlreadyExistingCollaborator_ShouldNotAdd()
    {
        // Arrange
        var collaboratorRepository = new Mock<ICollaboratorRepository>();
        var collaboratorFactory = new Mock<ICollaboratorFactory>();
        var mapper = new Mock<IMapper>();

        var id = Guid.NewGuid();
        var period = new PeriodDateTime(DateTime.Now, DateTime.Now.AddDays(5));


        var collaborator = new Mock<ICollaborator>();

        collaboratorRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(collaborator.Object);


        var service = new CollaboratorService(collaboratorRepository.Object, collaboratorFactory.Object, mapper.Object);

        // Act
        await service.SubmitCollaboratorAsync(id, period);

        // Assert
        collaboratorRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        collaboratorFactory.Verify(r => r.Create(It.IsAny<CollaboratorDataModel>()), Times.Never);
        collaboratorRepository.Verify(r => r.AddAsync(It.IsAny<ICollaborator>()), Times.Never);
    }
}
