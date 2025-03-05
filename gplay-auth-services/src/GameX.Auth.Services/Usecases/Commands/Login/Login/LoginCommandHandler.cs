namespace gamex.Auth.Services;

public record LoginCommand(LoginRequestDto requestDto) : IRequest<Response<LoggedInResponseDto>>{}
public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Response<LoggedInResponseDto>>
{
    private readonly ILoginTokenService tokenService;
    private readonly IUserRepository _repo;
    private readonly IPlayerRepository _playerRepo;
    private readonly IUserActivityCacheService _activityCacheService;


    public LoginCommandHandler(
        ILoginTokenService tokenService, 
        IUserRepository repo,
        IPlayerRepository playerRepo,
        IUserActivityCacheService activityCacheService
        )
    {
        this.tokenService = tokenService;
        _repo = repo;
        _playerRepo = playerRepo;
        _activityCacheService = activityCacheService;
    }


    // Step 1: Get user using email/mobile and password
    // Step 2: Generate Token from that user
    // Step 3: Return Login Response
    public async Task<Response<LoggedInResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
         string md5Password = Security.CreateMD5Hash(request.requestDto.Password);
    
        // Step 1: Verfiy if user already exists
        User? user = string.IsNullOrWhiteSpace(request.requestDto.Email) ? 
            await _repo.GetUserByMobile(request.requestDto.Mobile.AsMobile())
            : await _repo.GetUserByEmail(request.requestDto.Email.AsEmail());

        if ( user is null)
        {
            // Log Failed login 
            await _activityCacheService.SetInCache(request.requestDto.Get(LoginAttempt.Failed, "User not registered"));
            return LoginErrors.UserNotFoundByEmailOrMobile(request.requestDto.Mobile, request.requestDto.Email);
        }


        // Step 2: Validate Password
        if (user.Password != md5Password) {
            
             // Log Failed login 
            await _activityCacheService.SetInCache(request.requestDto.Get(LoginAttempt.Failed, "Invalid user password"));

            return LoginErrors.InvalidPassword();
        }


        var userProfile = await _playerRepo.GetUserProfile(user.Id);

        // Step 3: Generate Token from that user
        var tokenResult = await tokenService.GetToken(user.Id, userProfile.PlayerId);
        if (tokenResult.IsFailure) 
            return Response<LoggedInResponseDto>.Error(tokenResult.Error.Code, tokenResult.Error.Message);
        


        // Step 4: Return Login Response
        var loginResponse = LoggedInResponseDto.Get(userProfile, tokenResult.Value.Token);



        await _activityCacheService.SetInCache(request.requestDto.Get(LoginAttempt.Success, "", userProfile.UserId));


        return loginResponse;
        
    }





}