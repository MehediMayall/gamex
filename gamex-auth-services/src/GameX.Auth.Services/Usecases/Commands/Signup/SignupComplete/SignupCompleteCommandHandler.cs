namespace gamex.Auth.Services;

public sealed record SignupCompleteCommand(SignupCompleteRequestDto OTPRequestDto) : IRequest<Response<LoggedInResponseDto>>;

public sealed class SignupCompleteCommandHandler : IRequestHandler<SignupCompleteCommand, Response<LoggedInResponseDto>>
{
    private readonly ICacheService cacheService;
    private readonly ILoginTokenService tokenService;
    private readonly IPlayerRepository _repoPlayer;
    private readonly IUserNameGenerator _userNameGenerator;
    private readonly IUnitOfWork unitOfWork;

    public SignupCompleteCommandHandler(ICacheService cacheService, 
        ILoginTokenService tokenService, 
        IPlayerRepository playerRepo,
        IUserNameGenerator userNameGenerator,
        IUnitOfWork unitOfWork)
    {
        this.cacheService = cacheService;
        this.tokenService = tokenService;
        _repoPlayer = playerRepo;
        _userNameGenerator = userNameGenerator;
        this.unitOfWork = unitOfWork;
    }



    // Step 1: Load User Details from Cache using UserID
    // Step 2: Validate OTP
    // Step 3: Create User in DB
    // Step 4: Get Token for User
    // Step 5: Return Response    
    public async Task<Response<LoggedInResponseDto>> Handle(SignupCompleteCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Load User Details from Cache using UserID
        var requestedUserResult = await cacheService.GetAsync<UserRegistrationRequestedDto>(request.OTPRequestDto.UserId.ToString());

        if (requestedUserResult.IsFailure)
            return requestedUserResult.Error;

        // Step 2: Validate OTP
        var requestedUser = requestedUserResult.Value;
        if (requestedUser.OTP.Equals(request.OTPRequestDto.OTP) == false) 
            return SignupRequestErrors.OTPVerificationFailed(request.OTPRequestDto.OTP);
        

        Player newPlayer = Player.Get(requestedUserResult.Value, RegistrationSourceIds.General);

        User newUser = User.Get(requestedUserResult.Value, request.OTPRequestDto.UserId, newPlayer.Id);
        newUser.UserName = _userNameGenerator.GetName();

        newPlayer.User = newUser;

        // Step 3: Create User in DB
        await _repoPlayer.Add(newPlayer);

        // DB Commit
        var sqlCommitResult = await unitOfWork.SaveChangesAsync();

        if (sqlCommitResult.IsFailure && sqlCommitResult.Error.Code != nameof(PostgresErrorCode.UniqueViolation))
            return sqlCommitResult.Error;
        
        var userProfile = await _repoPlayer.GetUserProfile(newUser.Id);

        // Step 4: Get Token for User
        var tokenResult = await tokenService.GetToken(request.OTPRequestDto.UserId, userProfile.PlayerId );
        if (tokenResult.IsFailure) 
            return tokenResult.Error;


        // Step 5: Return Response
        return LoggedInResponseDto.Get(userProfile,  tokenResult.Value.Token);
    }

     
}
