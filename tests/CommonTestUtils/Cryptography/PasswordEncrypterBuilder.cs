using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtils.Cryptography;

public class PasswordEncrypterBuilder
{

    private readonly Mock<IPasswordEncrypter> _mock;
    
    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncrypter>();
        _mock.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>()))
            .Returns("mocked_encrypted_password");
    }
    
    public PasswordEncrypterBuilder Verify(string password)
    {
        if (string.IsNullOrWhiteSpace(password) == false)
        {
            _mock.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>()))
                .Returns(true);
        }
      
        return this;
    }
    public IPasswordEncrypter Build() => _mock.Object;
    
}