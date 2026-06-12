using FluentValidation;
using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Commands.DeleteGoal;

public class DeleteGoalCommandHandler(
    IValidator<DeleteGoalCommand> validator,
    IGoalRepository goalRepository)
:IRequestHandler<DeleteGoalCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await goalRepository.GetAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        if (goal is null)
            return Result<bool>.NotfoundFailure("هدف مورد نظر یافت نشد.");

        var deleteGoalResult = await goalRepository.DeleteAsync(goal, cancellationToken);
        if (!deleteGoalResult)
            return Result<bool>.GeneralFailure();
        
        return Result<bool>.Success(true);
    }
}