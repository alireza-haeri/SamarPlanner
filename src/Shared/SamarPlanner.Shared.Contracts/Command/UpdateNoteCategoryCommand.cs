using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record UpdateNoteCategoryCommand(Guid UserId,Guid NoteCategoryId, string Title) : IRequest<Result<bool>>;