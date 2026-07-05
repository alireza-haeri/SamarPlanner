using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Commands.DeleteNoteCategory;

public class DeleteNoteCategoryCommandHandler(INoteCategoryRepository noteCategoryRepository)
: IRequestHandler<DeleteNoteCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteNoteCategoryCommand request, CancellationToken cancellationToken)
    {
        var noteCategory =
            await noteCategoryRepository.GetByIdAsyncAsTracking(request.UserId, request.NoteCategoryId, cancellationToken);
        if(noteCategory is null)
            return Result<bool>.NotfoundFailure("دسته بندی یاداشت یافت نشد.");
        
        var deleteResult = await noteCategoryRepository.DeleteAsync(noteCategory, cancellationToken);
        if(!deleteResult)
            return Result<bool>.GeneralFailure("خطایی در حذف دسته بندی یاداشت رخ داد.");
        
        return Result<bool>.Success(true);
    }
}