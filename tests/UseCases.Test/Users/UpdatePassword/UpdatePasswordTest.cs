using CashFlow.Application.UseCases.Users.ChangePassword;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Cryptography;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using Shouldly;

namespace UseCases.Test.Users.UpdatePassword;

public class UpdatePasswordTest
{
    public class UpdatePasswordValidatorTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            var useCase = CreateUseCase(user, request.Password);
            var act = async () => await useCase.Execute(request);
            await act.ShouldNotThrowAsync();
        }
        
        [Fact]
        public async Task Error_Password_Empty()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.Password = string.Empty;
            var useCase = CreateUseCase(user, request.Password);
            var act = async () => await useCase.Execute(request);
            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }
        
        [Fact]
        public async Task Error_Password_Null()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.Password = null;
            var useCase = CreateUseCase(user, request.Password);
            var act = async () => await useCase.Execute(request);
            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }
        
      private static UpdateUserPasswordUseCase CreateUseCase(User user, string? newPassword = null)
      {
          var loggedUser =  LoggedUserBuilder.Build(user);
          var passwordEncrypter = new PasswordEncrypterBuilder().Verify(newPassword).Build();
          var repository = UserUpdateOnlyRepositoryBuilder.Build(user);
          var unitOfWork =  UnitOfWorkBuilder.Build();
          return new UpdateUserPasswordUseCase(loggedUser, passwordEncrypter, repository, unitOfWork);
      }
    }
}