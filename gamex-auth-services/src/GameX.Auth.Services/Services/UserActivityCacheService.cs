namespace gamex.Auth.Services;

public sealed class UserActivityCacheService (ICacheService cacheService): IUserActivityCacheService
{
    public async Task<bool> SetInCache(LoginActivityDto loginActivityDto) 
    {
        try 
        {
            string key = "UserLoginActivity";
            var cacheResult =  await cacheService.GetAsync<List<UserLogActivity>>(key);

            List<UserLogActivity> userLogActivities;

            if (cacheResult.IsFailure)
                userLogActivities = new List<UserLogActivity>();
            else 
                userLogActivities = cacheResult.Value;

            var logResult = UserLogActivity.New(loginActivityDto);
            if (logResult.IsFailure) 
                return false;

            userLogActivities.Add(logResult.Value);
            await cacheService.SetAsync(key, userLogActivities);
            
            return true;
        }
        catch(Exception ex) { 
            Log.Error(ex.GetAllExceptions()); 
            return false;
        }
    }
}