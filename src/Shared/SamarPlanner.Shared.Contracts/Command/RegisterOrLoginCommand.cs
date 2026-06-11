using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public sealed record RegisterOrLoginCommand(string PhoneNumber, string Password)
    : IRequest<Result<RegisterOrLoginCommandResponse>>;

public sealed record RegisterOrLoginCommandResponse(string Token, int  ExpireInMinutes);