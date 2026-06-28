using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Commands.AchieveGoal;

public class AchieveGoalCommandHandler (IGoalRepository goalRepository)
:IRequestHandler<AchieveGoalCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(AchieveGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await goalRepository.GetAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        if (goal is null)
            return Result<bool>.NotfoundFailure("هدف مورد نظر یافت نشد.");
        
        var childGoals = await goalRepository.GetNotRolledOverChildGoalsAsTrackingAsync(request.GoalId, request.UserId, cancellationToken);
        childGoals.ForEach(g => g.Achieve());
        
        goal.Achieve();
        
        var updateResult = await goalRepository.UpdateAsync(goal, cancellationToken);
        if(!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت هدف مورد نظر رخ داده است.");
        
        return Result<bool>.Success(true);
    }
}