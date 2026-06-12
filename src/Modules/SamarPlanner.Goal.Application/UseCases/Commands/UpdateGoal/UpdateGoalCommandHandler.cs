using FluentValidation;
using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Commands.UpdateGoal;

public class UpdateGoalCommandHandler(
    IValidator<UpdateGoalCommand> validator,
    IGoalRepository goalRepository)
    : IRequestHandler<UpdateGoalCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await goalRepository.GetAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        if (goal is null)
            return Result<bool>.NotfoundFailure("هدف مورد نظر یافت نشد.");

        goal.Update(request.Title, request.Description, request.GoalType, request.Priority, request.PeriodStart,
            request.PeriodEnd, request.ParentGoalId);
        var updateResult = await goalRepository.UpdateAsync(goal, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure();

        return Result<bool>.Success(true);
    }
}