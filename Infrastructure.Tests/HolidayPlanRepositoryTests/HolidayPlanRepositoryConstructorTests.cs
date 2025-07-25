using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryConstructorTests
{
    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange
        DbContextOptions<AbsanteeContext> options = new DbContextOptions<AbsanteeContext>();
        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>(options);
        var mockMapper = new Mock<IMapper>();

        //Act
        new HolidayPlanRepositoryEF(contextDouble.Object, mockMapper.Object);

        //Assert
    }
}