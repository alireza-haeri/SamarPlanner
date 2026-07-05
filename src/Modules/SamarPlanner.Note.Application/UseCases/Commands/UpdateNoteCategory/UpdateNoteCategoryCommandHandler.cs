using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Commands.UpdateNoteCategory;

public class UpdateNoteCategoryCommandHandler(INoteCategoryRepository noteCategoryRepository)
: IRequestHandler<UpdateNoteCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateNoteCategoryCommand request, CancellationToken cancellationToken)
    {
        var noteCategory =
            await noteCategoryRepository.GetByIdAsyncAsTracking(request.UserId, request.NoteCategoryId, cancellationToken);
        if(noteCategory is null)
            return Result<bool>.NotfoundFailure("دسته بندی یاداشت یافت نشد.");

        noteCategory.Update(request.Title);
        
        var updateResult = await noteCategoryRepository.UpdateAsync(noteCategory, cancellationToken);
        if(!updateResult)
            return Result<bool>.GeneralFailure("خطایی در بروزرسانی دسته بندی یاداشت رخ داد.");
        
        return Result<bool>.Success(true);
    }
}