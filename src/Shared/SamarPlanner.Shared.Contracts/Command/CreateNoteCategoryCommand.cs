#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record CreateNoteCategoryCommand(Guid UserId, string Title) : IRequest<Result<Guid>>;