using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

namespace Infrastructure.Tests.CollaboratorRepositoryTests;

public class CollaboratorRepositoryConstructorTests
{
    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange
        DbContextOptions<AbsanteeContext> options = new DbContextOptions<AbsanteeContext>();
        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>(options);
        var mockMapper = new Mock<IMapper>();

        //Act
        new CollaboratorRepositoryEF(contextDouble.Object, mockMapper.Object);

        //Assert
    }
}