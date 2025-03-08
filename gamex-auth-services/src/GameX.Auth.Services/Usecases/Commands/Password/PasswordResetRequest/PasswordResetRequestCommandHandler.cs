namespace gamex.Auth.Services;

public record PasswordResetRequestCommand(PasswordResetRequestDto requestDto) : IRequest<Response<PasswordResetRequestResponseDto>>{}
public sealed class PasswordResetRequestCommandHandler : IRequestHandler<PasswordResetRequestCommand, Response<PasswordResetRequestResponseDto>>
{
    private readonly IPlayerRepository _repo;
    private readonly ICacheService _cacheService;
    private readonly MassTransit.IPublishEndpoint _publisher;
    private readonly ICodeGenerationService _otpService;

    public PasswordResetRequestCommandHandler(
        IPlayerRepository repo,
        ICacheService cacheService,  
        MassTransit.IPublishEndpoint publisher, 
        ICodeGenerationService otpService)
    {
        _repo = repo;
        _cacheService = cacheService;
        _publisher = publisher;
        _otpService = otpService;
    }


    // Step 1: Get user using email/mobile and password
    // Step 2: Generate OTP
    // Step 3: Cache OTP 
    // Step 4: Send OTP Notification
    public async Task<Response<PasswordResetRequestResponseDto>> Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Get user using email/mobile and password

        // Step 1: Verfiy if user already exists
        var userProfile = string.IsNullOrWhiteSpace(request.requestDto.Email) ? 
            await _repo.GetUserProfileByMobile(request.requestDto.Mobile.AsMobile())
            : await _repo.GetUserProfileByEmail(request.requestDto.Email.AsEmail());

        if ( userProfile is null)
            return LoginErrors.UserNotFoundByEmailOrMobile(request.requestDto.Mobile, request.requestDto.Email);

        // Step 2: Generate OTP
        var otpResult = _otpService.GenerateOTP();
        if (otpResult.IsFailure) 
            return otpResult.Error;


        var resendRequest = new OTPResendRequestedDto(
            userProfile.FirstName,
            userProfile.LastName,
            userProfile.Email,
            userProfile.Mobile,
            otpResult.Value
        );

        // Step 3: Cache OTP 
        await _cacheService.SetAsync(
                userProfile.UserId.ToString(),
                resendRequest
            );

        // Step 4: Send OTP Notification
        await _publisher.Publish(resendRequest);

        // Step 5: Return Response
        return new PasswordResetRequestResponseDto(userProfile.UserId);
        
    }



}