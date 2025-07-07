using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Update;

public class UpdateUserProfileUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository updateOnlyRepository, IUsersReadOnlyRepository readOnlyRepository, IUnitOfWork unitOfWork): IUpdateUserProfileUseCase
{
   public async Task Execute(RequestUpdateUserProfileJson request)
    {
        var user = await loggedUser.Get();
        await Validate(request, user.Email);
        var userExists = await updateOnlyRepository.GetById(user.Id);
        userExists.Name = request.Name;
        userExists.Email = request.Email;
        updateOnlyRepository.Update(userExists);
        await unitOfWork.Commit();
    }
    
    private async Task Validate(RequestUpdateUserProfileJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();
        var result = validator.Validate(request);
        if (currentEmail.Equals(request.Email) == false)
        {
            var userExists = await readOnlyRepository.ExistsActiveUserWithEmail(request.Email);
            if(userExists) result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}