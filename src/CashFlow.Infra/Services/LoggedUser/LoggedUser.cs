using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.Services.LoggedUser;

public class LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider): ILoggedUser
{
    public async Task<User> Get()
    {
        string token = tokenProvider.TokenRequest();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        return await dbContext.Users.AsNoTracking().FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}  