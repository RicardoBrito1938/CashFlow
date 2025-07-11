using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Users.Delete;

public class DeleteUserUseCase(IUnitOfWork unitOfWork, IUsersWriteOnlyRepository writeOnlyRepository, ILoggedUser loggedUser): IDeleteUserUseCase
{
    public async Task Execute()
    {
        var user = await loggedUser.Get();
        await writeOnlyRepository.Delete(user);
        await unitOfWork.Commit();
    }
}