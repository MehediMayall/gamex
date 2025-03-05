namespace gamex.Auth.Services;

public sealed record OTPResendCommand(OTPResendRequestDto requestDto) : IRequest<Response<OTPResendResponseDto>>{}
public sealed class OTPResendCommandHandler : IRequestHandler<OTPResendCommand, Response<OTPResendResponseDto>>
{
    private readonly ICacheService cacheService;
    private readonly MassTransit.IPublishEndpoint publisher;
    private readonly ICodeGenerationService otpService;
    private readonly IUserRepository _repo;
    private readonly IPlayerRepository _repoPlayer;

    public OTPResendCommandHandler(
        ICacheService cacheService, 
        MassTransit.IPublishEndpoint publisher, 
        ICodeGenerationService otpService,
        IUserRepository userRepository,
        IPlayerRepository playerRepo
        )
    {
        this.cacheService = cacheService;
        this.publisher = publisher;
        this.otpService = otpService;
        _repo = userRepository;
        _repoPlayer = playerRepo;
    }


    // Generate OTP and Send OTP Notification
    // Step 1: Generate OTP
    // Step 2: Initialize New UserId and Register Response Dto
    // Step 3: Cache Register Request
    // Step 4: Send OTP Notification
    // Step 5: Return UserID Only
    public async Task<Response<OTPResendResponseDto>> Handle(OTPResendCommand request, CancellationToken cancellationToken)
    { 
        // Step 1: Generate OTP
        var otpResult = otpService.GenerateOTP();
        if (otpResult.IsFailure) 
            return otpResult.Error;

        Guid userid = request.requestDto.UserId;

        // Initialize Register Request

        // RESEND TYPE = SIGNUP
        if ( nameof(OTPResendTypes.SIGNUP) == request.requestDto.ResendType )        
        {
            var userResult = await cacheService.GetAsync<UserRegistrationRequestedDto>(userid.ToString());
            UserRegistrationRequestedDto user = userResult.Value;

            user = new UserRegistrationRequestedDto () {
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                Email = user.Email, 
                Mobile = user.Mobile,
                DateOfBirth = user.DateOfBirth,
                Password = user.Password, 
                UserName = user.UserName,
                OTP = otpResult.Value
            };


            // Step 4: Cache Register Request
            await cacheService.SetAsync(
                userid.ToString(),  
                user
            );

   
            // Step 4: Send OTP Notification
            await publisher.Publish(user);

            // Step 5: Return UserID Only
            return  new OTPResendResponseDto("Success");
        }
        // RESEND TYPE = LOGIN
        else if ( nameof(OTPResendTypes.LOGIN) == request.requestDto.ResendType )
        {
            var user = await _repoPlayer.GetUserProfile(userid);
            if (user == null) 
                return LoginErrors.UserNotFoundById(userid);


            var resendRequest = new OTPResendRequestedDto(
                user.FirstName,
                user.LastName,
                user.Email,
                user.Mobile,
                otpResult.Value
            );


            // Step 4: Cache Register Request
            await cacheService.SetAsync(
                userid.ToString(),  
                resendRequest
            );

            // Step 4: Send OTP Notification
            await publisher.Publish(resendRequest);

            // Step 5: Return UserID Only
            return  new OTPResendResponseDto("Success");
        }
        
        
        return OTPResendErrors.InvalidResendType();      
    }
}
