namespace gamex.Auth.Services;

public record OTPVerificationCommand(OTPVerificationRequestDto requestDto) : IRequest<Response<OTPVerificationResponseDto>>{}
public sealed class OTPVerificationCommandHandler : IRequestHandler<OTPVerificationCommand, Response<OTPVerificationResponseDto>>
{
    private readonly ICacheService _cacheService;

    public OTPVerificationCommandHandler(
        ICacheService cacheService)
    {
        _cacheService = cacheService;
    }


    // Step 1: Load User Details from Cache using UserID
    // Step 2: Validate OTP
    // Step 3: Return Response
    public async Task<Response<OTPVerificationResponseDto>> Handle(OTPVerificationCommand request, CancellationToken cancellationToken)
    {
 
        // Step 1: Load User Details from Cache using UserID
        var requestedUserResult = await _cacheService.GetAsync<UserRegistrationRequestedDto>(request.requestDto.UserId.ToString());

        if (requestedUserResult.IsFailure)
            return requestedUserResult.Error;

        // Step 2: Validate OTP
        var requestedUser = requestedUserResult.Value;
        if (requestedUser.OTP.Equals(request.requestDto.OTP) == false) 
            return SignupRequestErrors.OTPVerificationFailed(request.requestDto.OTP);

        // Step 3: Return Response
        return new OTPVerificationResponseDto("Success");
        
    }



}