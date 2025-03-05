namespace gamex.Auth.Services;

public sealed record SignupRequestCommand(SignupRequestDto requestDto) : IRequest<Response<SignupRequestResponseDto>>{}
public sealed class SignupRequestCommandHandler : IRequestHandler<SignupRequestCommand, Response<SignupRequestResponseDto>>
{
    private readonly ICacheService cacheService;
    private readonly MassTransit.IPublishEndpoint publisher;
    private readonly ICodeGenerationService otpService;
    private readonly IUserRepository _repo;

    public SignupRequestCommandHandler(
        ICacheService cacheService, 
        MassTransit.IPublishEndpoint publisher, 
        ICodeGenerationService otpService,
        IUserRepository userRepository
        )
    {
        this.cacheService = cacheService;
        this.publisher = publisher;
        this.otpService = otpService;
        _repo = userRepository;
    }


    // Generate OTP and Send OTP Notification
    // Step 1: Verfiy if user already exists
    // Step 2: Generate OTP
    // Step 3: Initialize New UserId and SignupRequest Response Dto
    // Step 4: Cache SignupRequest Request
    // Step 5: Send OTP Notification
    // Step 6: Return UserID Only
    public async Task<Response<SignupRequestResponseDto>> Handle(SignupRequestCommand request, CancellationToken cancellationToken)
    {
        
        // Step 1: Verfiy if user already exists
        bool IsUserAlreadyExists = string.IsNullOrWhiteSpace(request.requestDto.Email) ? 
            await _repo.IsUserExistsByMobile(request.requestDto.Mobile.AsMobile())
            : await _repo.IsUserExistsByEmail(request.requestDto.Email.AsEmail());

        if ( IsUserAlreadyExists )
            return SignupRequestErrors.UserAlreadyExists();

        // Step 2: Generate OTP
        var otpResult = otpService.GenerateOTP();
        if (otpResult.IsFailure) 
            return otpResult.Error;

        // Step 3: Initialize New UserId and SignupRequest Response Dto
        var SignupRequestResponse = new SignupRequestResponseDto(Guid.NewGuid());

        // Initialize SignupRequest Request
        var SignupRequestRequest = request.requestDto.GetUserRegistrationRequestedDto(otpResult.Value);

        // Step 4: Cache SignupRequest Request
        await cacheService.SetAsync(
                SignupRequestResponse.UserId.ToString(),  
                SignupRequestRequest
            );

        // Step 5: Send OTP Notification
        await publisher.Publish(SignupRequestRequest);

        // Step 6: Return UserID Only
        return  SignupRequestResponse;
    }
}