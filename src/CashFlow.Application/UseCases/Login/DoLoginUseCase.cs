using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Login;

public class DoLoginUseCase(IUsersReadOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator): IDoLoginUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await repository.GetByEmail(request.Email);
        if( user is null)
        {
            throw new InvalidLoginException();
        }
        
        var passwordMatch = passwordEncrypter.Verify(request.Password, user.Password);
        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = accessTokenGenerator.Generate(user),
        };
    }
}