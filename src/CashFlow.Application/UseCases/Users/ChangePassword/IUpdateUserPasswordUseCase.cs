using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Users.ChangePassword;

public interface IUpdateUserPasswordUseCase
{
    Task Execute(RequestUpdateUserPasswordJson request);
}