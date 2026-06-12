using FluentValidation;
using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Commands.CreateGoal;

public class CreateGoalCommandHandler(
    IValidator<CreateGoalCommand> validator,
    IGoalRepository goalRepository
)
    : IRequestHandler<CreateGoalCommand, Result<CreateGoalCommandResponse>>
{
    public async Task<Result<CreateGoalCommandResponse>> Handle(CreateGoalCommand request,
        CancellationToken cancellationToken)
    {
        var goal = Core.Entities.Goal.Create(request.UserId, request.Title, request.Description, request.GoalType,
            request.Priority, request.PeriodStart, request.PeriodEnd, request.ParentGoalId);

        var goalIdResult = await goalRepository.CreateAsync(goal, cancellationToken);
        if (goalIdResult is null)
            return Result<CreateGoalCommandResponse>.GeneralFailure();

        return Result<CreateGoalCommandResponse>.Success(new CreateGoalCommandResponse(goalIdResult.Value));
    }
}