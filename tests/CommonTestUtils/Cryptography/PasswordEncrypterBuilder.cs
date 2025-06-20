using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtils.Cryptography;

public class PasswordEncrypterBuilder
{
    public static IPasswordEncrypter Build()
    {
        var mock = new Mock<IPasswordEncrypter>();
        mock.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>()))
            .Returns("mocked_encrypted_password");
        return mock.Object;
    }
}