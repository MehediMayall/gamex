namespace gamex.Auth.Services;

public interface IPlayerRepository: IRepository<Player>
{
    Task<UserProfileResponseDto?> GetUserProfile(Guid userId);
    Task<UserProfileResponseDto?> GetUserProfileByEmail(Email email);
    Task<UserProfileResponseDto?> GetUserProfileByMobile(Mobile mobile);

}