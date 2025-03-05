namespace gamex.Auth.Services;

public interface IUserRepository: IRepository<User> { 
    Task<bool> IsUserExistsByEmail(Email Email);
    Task<bool> IsUserExistsByMobile(Mobile mobile);
    Task<User?> GetUserByEmail(Email email);
    Task<User?> GetUserByMobile(Mobile mobile);

}