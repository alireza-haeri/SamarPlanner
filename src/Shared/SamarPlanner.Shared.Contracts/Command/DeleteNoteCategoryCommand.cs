#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record DeleteNoteCategoryCommand(Guid UserId, Guid NoteCategoryId) : IRequest<Result<bool>>;