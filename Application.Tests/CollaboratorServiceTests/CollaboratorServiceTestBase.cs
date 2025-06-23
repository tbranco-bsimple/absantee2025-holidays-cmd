/* using Moq;
using Domain.IRepository;
using Application.Services;
using Domain.Factory;
using Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.CollaboratorServiceTests
{
    public abstract class CollaboratorServiceTestBase
    {
        protected Mock<ICollaboratorRepository> CollaboratorRepositoryDouble;
        protected Mock<ICollaboratorFactory> CollaboratorFactoryDouble;
        protected AbsanteeContext _context;
        protected Mock<IMapper> MapperDouble;

        protected CollaboratorService CollaboratorService;

        private static readonly Random _random = new();

        protected CollaboratorServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AbsanteeContext(options);

            CollaboratorRepositoryDouble = new Mock<ICollaboratorRepository>();
            CollaboratorFactoryDouble = new Mock<ICollaboratorFactory>();
            MapperDouble = new Mock<IMapper>();

            CollaboratorService = new CollaboratorService(
                CollaboratorRepositoryDouble.Object,
                CollaboratorFactoryDouble.Object,
                MapperDouble.Object
            );
        }
    }
} */