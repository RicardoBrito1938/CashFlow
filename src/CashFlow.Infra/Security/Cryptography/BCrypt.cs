using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infra.Security.Cryptography;

public class BCrypt: IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        return BC.HashPassword(password);
    }
}
