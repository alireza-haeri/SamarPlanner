using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.CreateTask;

public class CreateTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<CreateTaskCommand, Result<CreateTaskCommandResponse>>
{
    public async Task<Result<CreateTaskCommandResponse>> Handle(CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = Core.Entities.Task.Create
        (
            userId: request.UserId,
            title: request.Title,
            description: request.Description,
            date: request.Date,
            defaultTime: request.DefaultTime,
            priority: request.Priority,
            type: request.Type,
            repeatPattern: request.RepeatPattern?.ToRepeatPattern(),
            parentGoalId: request.ParentGoalId
        );

        var createResult = await taskRepository.CreateAsync(task, cancellationToken);
        if (!createResult)
            return Result<CreateTaskCommandResponse>.GeneralFailure();

        return Result<CreateTaskCommandResponse>.Success(new CreateTaskCommandResponse(task.Id));
    }
}