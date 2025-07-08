using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.ChangePassword;

public class UpdateUserPasswordUseCase: IUpdateUserPasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncrypter _passwordEncrypter;
    
    public UpdateUserPasswordUseCase(ILoggedUser loggedUser, IPasswordEncrypter passwordEncrypter, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncrypter = passwordEncrypter;
    }
    
    public async Task Execute(RequestUpdateUserPasswordJson request)
    {
        var user = await _loggedUser.Get();
        Validate(request, user);
        var updatedUser = await _repository.GetById(user.Id);
        updatedUser.Password = _passwordEncrypter.Encrypt(request.NewPassword);
         _repository.Update(updatedUser);
         await _unitOfWork.Commit();
    }
    
    private void Validate(RequestUpdateUserPasswordJson request, Domain.Entities.User user)
    {
        var validator = new UpdatePasswordValidator();
        var result = validator.Validate(request);
        var passwordMatch = _passwordEncrypter.Verify(request.Password, user.Password);
        if (passwordMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PASSWORD_MUST_MATCH));
        }

        if (result.IsValid != false) return;
        var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errors);
    }
}