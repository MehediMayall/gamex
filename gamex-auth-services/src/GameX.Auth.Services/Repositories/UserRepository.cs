namespace gamex.Auth.Services;

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context): base(context) => _context = context;
    
    public async Task<User?> GetUserByEmail(Email email) =>
        await (
            from user in _context.Users 
            join player in _context.Players on user.PlayerId equals player.Id
            where player.Email == email 
            && user.IsActive == true
            select user
        ).FirstOrDefaultAsync();

    public async Task<User?> GetUserByMobile(Mobile mobile) =>
        await (
            from user in _context.Users 
            join player in _context.Players on user.PlayerId equals player.Id
            where player.Mobile == mobile 
            && user.IsActive == true
            select user
        ).FirstOrDefaultAsync();



    public async Task<bool> IsUserExistsByEmail(Email Email) =>
        (
            from user in _context.Users 
            join player in _context.Players on user.PlayerId equals player.Id
            where player.Email == Email 
            && user.IsActive == true
            select new {
                user.Id
            }
        ).Any();

    public async Task<bool> IsUserExistsByMobile(Mobile mobile) =>
        (
            from user in _context.Users 
            join player in _context.Players on user.PlayerId equals player.Id
            where player.Mobile == mobile 
            && user.IsActive == true
            select new {
                user.Id
            }
        ).Any();
  
        
   
}
