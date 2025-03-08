namespace gamex.Auth.Services;

public interface ILoginTokenService{
    Task<Result<TokenDescriptionDto>> GetToken(Guid userId, Guid playerId);
}
