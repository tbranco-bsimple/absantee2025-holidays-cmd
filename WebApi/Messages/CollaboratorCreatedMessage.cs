using Domain.Models;

namespace WebApi.Messages;

public record CollaboratorCreatedMessage(Guid Id, PeriodDateTime PeriodDateTime);