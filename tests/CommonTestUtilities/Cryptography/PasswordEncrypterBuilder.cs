using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();

        _mock.Setup(config => config.Encrypt(It.IsAny<string>())).Returns("$2a$11$wAB2qetBWYpO5H.ktbzsMODzaxghVtFvVtOTMf7vFkrC.7RuchyO.");
    }

    public PasswordEncrypterBuilder Verify(string? password)
    {
        if(string.IsNullOrWhiteSpace(password) == false)
        {
            _mock.Setup(config => config.Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this;
    }

    public IPasswordEncripter Build() => _mock.Object;
}
