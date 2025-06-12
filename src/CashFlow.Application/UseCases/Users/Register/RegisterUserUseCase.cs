using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Users.Register;

public class RegisterUserUseCase(IMapper mapper, IPasswordEncrypter passwordEncrypter): IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        var user = mapper.Map<User>(request);
        user.Password = passwordEncrypter.Encrypt(request.Password);

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        if (result.IsValid != false) return;
        var errorMessages = result.Errors
            .Select(error => error.ErrorMessage)
            .ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}