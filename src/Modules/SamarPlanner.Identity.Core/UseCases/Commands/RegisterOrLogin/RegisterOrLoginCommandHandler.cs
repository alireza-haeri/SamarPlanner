using FluentValidation;
using MediatR;
using SamarPlanner.Identity.Core.Abstractions;
using SamarPlanner.Identity.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Identity.Core.UseCases.Commands.RegisterOrLogin;

public class RegisterOrLoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokenService jwtToken,
    IValidator<RegisterOrLoginCommand> validator)
    : IRequestHandler<RegisterOrLoginCommand, Result<RegisterOrLoginCommandResponse>>
{
    public async Task<Result<RegisterOrLoginCommandResponse>> Handle(RegisterOrLoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.PhoneNumber, cancellationToken);
        if (user is null)
        {
            var userResult = User.Create(request.PhoneNumber);
            if (!userResult.IsSuccess)
                return Result<RegisterOrLoginCommandResponse>.GeneralFailure(userResult.ErrorMessage!);

            user = userResult.Response;
            var createResult = await userRepository.CreateAsync(user, request.Password, cancellationToken);
            if (!createResult.Succeeded)
                return Result<RegisterOrLoginCommandResponse>.ValidationFailure(createResult.Errors);
        }
        else
        {
            var checkPassword =
                await userRepository.CheckPasswordAsync(request.PhoneNumber, request.Password, cancellationToken);
            if (!checkPassword)
                return Result<RegisterOrLoginCommandResponse>.NotfoundFailure("کاربری با شماره تلفن و رمز مورد نظر یافت نشد!");

        }
        
        var token = jwtToken.GenerateToken(user);

        return Result<RegisterOrLoginCommandResponse>.Success(
            new RegisterOrLoginCommandResponse(token.Token, token.ExpiresInMinutes)
        );
    }
}