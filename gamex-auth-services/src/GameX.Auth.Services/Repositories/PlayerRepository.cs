namespace gamex.Auth.Services;

public sealed class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    private readonly UserDbContext _context;

    public PlayerRepository(UserDbContext context): base(context) => _context = context;
    

    public async Task<UserProfileResponseDto?> GetUserProfile(Guid userId) =>
        await (
            from player in _context.Players
            join user in _context.Users on player.Id equals user.PlayerId
            where user.Id == userId 
            && user.IsActive == true
            select new UserProfileResponseDto{
                PlayerId = player.Id,
                UserId = userId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Email = player.Email.Value,
                Mobile = player.Mobile.Value,
                UserName = user.UserName,
                DateOfBirth = player.DateOfBirth,
                City = player.City,
                Country = player.Country,
                AvatarName = player.AvatarName,
                ProfileImagename= player.ProfileImagename

            }
        ).FirstOrDefaultAsync();

    public async Task<UserProfileResponseDto?> GetUserProfileByEmail(Email email) =>
        await (
            from player in _context.Players
            join user in _context.Users on player.Id equals user.PlayerId
            where player.Email == email
            && user.IsActive == true
            select new UserProfileResponseDto{
                PlayerId = player.Id,
                UserId = user.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Email = player.Email.Value,
                Mobile = player.Mobile.Value,
                UserName = user.UserName,
                DateOfBirth = player.DateOfBirth,
                City = player.City,
                Country = player.Country,
                AvatarName = player.AvatarName,
                ProfileImagename= player.ProfileImagename


            }
        ).FirstOrDefaultAsync();

    public async Task<UserProfileResponseDto?> GetUserProfileByMobile(Mobile mobile) =>
        await (
            from player in _context.Players
            join user in _context.Users on player.Id equals user.PlayerId
            where player.Mobile == mobile
            && user.IsActive == true
            select new UserProfileResponseDto {
                PlayerId = player.Id,
                UserId = user.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Email = player.Email.Value,
                Mobile = player.Mobile.Value,
                UserName = user.UserName,
                DateOfBirth = player.DateOfBirth,
                City = player.City,
                Country = player.Country,
                AvatarName = player.AvatarName,
                ProfileImagename= player.ProfileImagename

            }
        ).FirstOrDefaultAsync();
  
        
   
}
