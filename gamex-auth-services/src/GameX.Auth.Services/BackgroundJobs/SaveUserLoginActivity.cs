using Quartz;

namespace gamex.Auth.Services;

public sealed class SaveUserLoginActivity : IJob
{
    private readonly IUserLogActivityRepository _userLogActivityRepo;
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public SaveUserLoginActivity(
        IUserLogActivityRepository userLogActivityRepo,
        ICacheService cacheService,
        IUnitOfWork unitOfWork
        )
    {
        _userLogActivityRepo = userLogActivityRepo;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        string key = "UserLoginActivity";
        try
        {
            var cacheResult =  await _cacheService.GetAsync<List<UserLogActivity>>(key);

            if (cacheResult.IsSuccess && cacheResult.Value.Any())  
            {
                await _userLogActivityRepo.AddRange(cacheResult.Value);
                var commitResult =  await _unitOfWork.SaveChangesAsync();
                if (commitResult.IsFailure) 
                    Log.Error(commitResult.Error.Message);
                
            }


        }
        catch(Exception ex) {Log.Error(ex.GetAllExceptions());}

        try{
            await _cacheService.SetAsync<List<UserLogActivity>>(key, new List<UserLogActivity>());
        }
        catch(Exception ex) {Log.Error(ex.GetAllExceptions());}
    }
}