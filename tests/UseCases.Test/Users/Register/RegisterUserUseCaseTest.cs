using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCases.Test.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Token.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorNameEmpty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.ShouldSatisfyAllConditions(
            e => e.GetErrors().ShouldHaveSingleItem(),
            e => e.GetErrors().ShouldContain(ResourceErrorMessages.NAME_EMPTY)
            );
    }

    [Fact]
    public async Task ErrorEmailAlreadyExists()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.ShouldSatisfyAllConditions(
            e => e.GetErrors().ShouldHaveSingleItem(),
            e => e.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_ALREADY_EXISTS)
            );
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var cryptography = new PasswordEncrypterBuilder().Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var unityOfWork = UnityOfWorkBuilder.Build();

        if(string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(mapper, cryptography, readRepository.Build(), writeRepository, tokenGenerator, unityOfWork);
    }
}
