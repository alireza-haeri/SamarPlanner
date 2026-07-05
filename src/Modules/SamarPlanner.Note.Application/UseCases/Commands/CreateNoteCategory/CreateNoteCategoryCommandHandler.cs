using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Commands.CreateNoteCategory;

public class CreateNoteCategoryCommandHandler(INoteCategoryRepository noteCategoryRepository)
    : IRequestHandler<CreateNoteCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateNoteCategoryCommand request, CancellationToken cancellationToken)
    {
        var noteCategory = NoteCategory.Create(request.UserId,request.Title);
        
        var createResult = await noteCategoryRepository.CreateAsync(noteCategory, cancellationToken);
        if(createResult is null)
            return Result<Guid>.GeneralFailure("خطایی در ایجاد دسته بندی یاداشت رخ داد.");
        
        return Result<Guid>.Success(createResult.Value);
    }
}