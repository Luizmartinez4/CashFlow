using CashFlow.Application.UseCases.Login;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCases.Test.Login.DoLogin;
public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(user.Name);
        result.Token.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorUserNotFound()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<InvalidLoginException>();

        result.ShouldSatisfyAllConditions(
            e => e.GetErrors().ShouldHaveSingleItem(),
            e => e.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
        );
    }

    [Fact]
    public async Task ErrorPasswordNotFound()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<InvalidLoginException>();

        result.ShouldSatisfyAllConditions(
            e => e.GetErrors().ShouldHaveSingleItem(),
            e => e.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
        );
    }

    private DoLoginUseCase CreateUseCase(CashFlow.Domain.Entities.User user, string? password = null)
    {
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        var cryptography = new PasswordEncrypterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        return new DoLoginUseCase(readRepository, cryptography, tokenGenerator);
    }
}
