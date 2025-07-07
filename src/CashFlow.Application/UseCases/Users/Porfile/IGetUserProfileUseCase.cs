using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.Porfile;

public interface IGetUserProfileUseCase
{
    Task<ResponseProfileJson> Execute();
}