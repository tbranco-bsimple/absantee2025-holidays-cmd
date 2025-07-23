using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.CollaboratorRepositoryTests;

public class CollaboratorRepositoryGetByIdTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenSearchingById_ThenReturnsCollaboratorWithId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
            .Options;

        using var context = new AbsanteeContext(options);

        var collaborator1 = new Mock<ICollaborator>();
        var guid1 = Guid.NewGuid();
        collaborator1.Setup(c => c.Id).Returns(guid1);
        var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
        context.Collaborators.Add(collaboratorDM1);

        var collaborator2 = new Mock<ICollaborator>();
        var guid2 = Guid.NewGuid();
        collaborator2.Setup(c => c.Id).Returns(guid2);
        var collaboratorDM2 = new CollaboratorDataModel(collaborator2.Object);
        context.Collaborators.Add(collaboratorDM2);

        var collaborator3 = new Mock<ICollaborator>();
        var guid3 = Guid.NewGuid();
        collaborator3.Setup(c => c.Id).Returns(guid3);
        var collaboratorDM3 = new CollaboratorDataModel(collaborator3.Object);
        context.Collaborators.Add(collaboratorDM3);

        await context.SaveChangesAsync();

        var expected = new Mock<ICollaborator>().Object;

        _mapper.Setup(m => m.Map<CollaboratorDataModel, Collaborator>(
            It.Is<CollaboratorDataModel>(t =>
                t.Id == collaboratorDM1.Id
            )))
            .Returns(new Collaborator(collaboratorDM1.Id, collaboratorDM1.PeriodDateTime));

        var collaboratorRepository = new CollaboratorRepositoryEF(context, _mapper.Object);
        //Act 
        var result = collaboratorRepository.GetById(guid1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(collaboratorDM1.Id, result.Id);
    }

    [Fact]
    public async Task WhenSearchingByIdWithNoCollaborators_ThenReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
            .Options;

        using var context = new AbsanteeContext(options);

        var collaborator1 = new Mock<ICollaborator>();
        var guid1 = Guid.NewGuid();
        collaborator1.Setup(c => c.Id).Returns(guid1);
        var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
        context.Collaborators.Add(collaboratorDM1);

        var collaborator2 = new Mock<ICollaborator>();
        var guid2 = Guid.NewGuid();
        collaborator2.Setup(c => c.Id).Returns(guid2);
        var collaboratorDM2 = new CollaboratorDataModel(collaborator2.Object);
        context.Collaborators.Add(collaboratorDM2);

        var collaborator3 = new Mock<ICollaborator>();
        var guid3 = Guid.NewGuid();
        collaborator3.Setup(c => c.Id).Returns(guid3);
        var collaboratorDM3 = new CollaboratorDataModel(collaborator3.Object);
        context.Collaborators.Add(collaboratorDM3);

        await context.SaveChangesAsync();

        var expected = new Mock<ICollaborator>().Object;

        _mapper.Setup(m => m.Map<CollaboratorDataModel, Collaborator>(
            It.Is<CollaboratorDataModel>(t =>
                t.Id == collaboratorDM1.Id
            )))
           .Returns(new Collaborator(collaboratorDM1.Id, collaboratorDM1.PeriodDateTime));

        var collaboratorRepository = new CollaboratorRepositoryEF(context, _mapper.Object);
        //Act 
        var result = collaboratorRepository.GetById(Guid.NewGuid());

        //Assert
        Assert.Null(result);
    }
}
