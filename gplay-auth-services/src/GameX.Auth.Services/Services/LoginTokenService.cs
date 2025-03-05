using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;


namespace gamex.Auth.Services;

internal sealed class LoginTokenService : ILoginTokenService
{
    private readonly IValidator<JWTSettings> jwtValidator;
    private readonly JWTSettings jwtSettings;
    private TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);

    public LoginTokenService(IValidator<JWTSettings> jwtValidator, IOptions<JWTSettings> jwtSettings)
    {
        this.jwtValidator = jwtValidator;
        this.jwtSettings = jwtSettings.Value;
    }
    
    public async Task<Result<TokenDescriptionDto>> GetToken(Guid userId, Guid playerId)
    {

        // Validation

        if (userId == Guid.Empty) 
            return SignupRequestErrors.NullValue(userId);
        if (jwtSettings is null) 
            return SignupRequestErrors.JWTConfigNotFound();

        var validationResult = jwtValidator.Validate(jwtSettings);
        if (validationResult.IsValid == false)
            return Error.New(validationResult.Errors.FirstOrDefault().ErrorMessage);

        // Token Expiration Time
        ExpiryDuration = new TimeSpan(0, jwtSettings.AccessExpirationInMinute, 0);


        var claims = new List<Claim>
        {
            new Claim("sub", JsonSerializer.Serialize(new UserSessionDto(userId, playerId))),   
        };        

        // Audiences
        string[] audiences = jwtSettings.Audience.Split(";");
        claims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));        
        

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Issuer, claims,
            expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        if (string.IsNullOrEmpty(token))
            return SignupRequestErrors.TokenNotGenerated(tokenDescriptor);

        return new TokenDescriptionDto{
            Token = token,
            Expires = DateTime.Now.Add(ExpiryDuration),
            ExpiryDuration =  ExpiryDuration
        };
    }
}
