using Application.DTO;
using Domain.Models;

namespace InterfaceAdapters.IntegrationTests.Helpers;

public static class CollaboratorHelper
{
    private static readonly Random _random = new();

    public static CollaboratorDTO GenerateRandomCollaboratorDto()
    {
        var deactivationDate = DateTime.UtcNow.AddYears(_random.Next(5, 10));
        return new CollaboratorDTO
        {
            PeriodDateTime = new PeriodDateTime
            {
                _initDate = DateTime.UtcNow,
                _finalDate = deactivationDate
            }
        };
    }

    public static CollaboratorDTO GenerateRandomCollaboratorDtoWithDates(DateTime ini, DateTime end)
    {
        return new CollaboratorDTO
        {
            PeriodDateTime = new PeriodDateTime
            {
                _initDate = ini,
                _finalDate = end
            }
        };
    }
}
