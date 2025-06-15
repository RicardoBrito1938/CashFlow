using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Users.Register;

public class RegisterUserUseCase(
    IMapper mapper,
    IPasswordEncrypter passwordEncrypter,
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IUsersWriteOnlyRepository usersWriteOnlyRepository,
    IUnitOfWork unitOfWork,
    IAccessTokenGenerator accessTokenGenerator): IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        var user = mapper.Map<User>(request);
        user.Password = passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        
        await usersWriteOnlyRepository.Add(user);
        await unitOfWork.Commit();

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Token = accessTokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        var emailExists = await usersReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);
        if (emailExists)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }
        if (result.IsValid != false) return;
        var errorMessages = result.Errors
            .Select(error => error.ErrorMessage)
            .ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}