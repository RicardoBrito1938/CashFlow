using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Users.Porfile;

public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper): IGetUserProfileUseCase
{
    public async Task<ResponseProfileJson> Execute()
    {
        var user = await loggedUser.Get();
        return mapper.Map<ResponseProfileJson>(user);
    }
}