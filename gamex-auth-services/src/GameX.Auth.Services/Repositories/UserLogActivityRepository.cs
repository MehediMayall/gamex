namespace gamex.Auth.Services;

public sealed class UserLogActivityRepository : GenericRepository<UserLogActivity>, IUserLogActivityRepository
{
    
   public UserLogActivityRepository(UserDbContext dbContext) : base(dbContext)
   {
    
   }
}
