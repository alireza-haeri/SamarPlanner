using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Commands.RolloverGoal;

public class RolloverGoalCommandHandler(IGoalRepository goalRepository)
    : IRequestHandler<RolloverGoalCommand, Result<RolloverGoalCommandResult>>
{
    public async Task<Result<RolloverGoalCommandResult>> Handle(RolloverGoalCommand request,
        CancellationToken cancellationToken)
    {
        var goal = await goalRepository.GetAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        if (goal is null)
            return Result<RolloverGoalCommandResult>.NotfoundFailure("هدف مورد نظر یافت نشد.");

        var newGoal = goal.Rollover(request.PeriodStart, request.PeriodEnd);
        var updateResult = await goalRepository.UpdateAsync(goal, cancellationToken);
        var addResult = await goalRepository.CreateAsync(newGoal, cancellationToken);
        if (addResult is null || !updateResult)
            return Result<RolloverGoalCommandResult>.GeneralFailure("خطا در ایجاد هدف جدید رخ داد.");

        return Result<RolloverGoalCommandResult>.Success(new RolloverGoalCommandResult(newGoal.Id));
    }
}