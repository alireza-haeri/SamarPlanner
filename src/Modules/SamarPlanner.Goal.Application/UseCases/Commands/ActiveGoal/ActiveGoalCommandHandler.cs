using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Commands.ActiveGoal;

public class ActiveGoalCommandHandler (IGoalRepository goalRepository)
:IRequestHandler<ActiveGoalCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(ActiveGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await goalRepository.GetAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        if (goal is null)
            return Result<bool>.NotfoundFailure("هدف مورد نظر یافت نشد.");
        
        var childGoals = await goalRepository.GetNotRolledOverChildGoalsAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        childGoals.ForEach(g => g.Active());
        
        goal.Active();
        
        var updateResult = await goalRepository.UpdateAsync(goal, cancellationToken);
        if(!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت هدف مورد نظر رخ داده است.");
        
        return Result<bool>.Success(true);
    }
}