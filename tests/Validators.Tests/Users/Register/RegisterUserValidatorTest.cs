using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Users.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("         ")]
    [InlineData(null)]
    public void ErrorNameEmpty(string name)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            e => e.ShouldHaveSingleItem(),
            e => e.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY))
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData("         ")]
    [InlineData(null)]
    public void ErrorEmailEmpty(string email)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            e => e.ShouldHaveSingleItem(),
            e => e.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY))
        );
    }

    [Fact]
    public void ErrorEmailInvalid()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "luiz.com";

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            e => e.ShouldHaveSingleItem(),
            e => e.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID))
        );
    }

    [Fact]
    public void ErrorPasswordEmpty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            e => e.ShouldHaveSingleItem(),
            e => e.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD))
        );
    }
}
