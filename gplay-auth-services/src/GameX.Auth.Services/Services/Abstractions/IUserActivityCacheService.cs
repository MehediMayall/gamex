namespace gamex.Auth.Services;

public interface IUserActivityCacheService  {
    Task<bool> SetInCache(LoginActivityDto loginActivityDto);
}
