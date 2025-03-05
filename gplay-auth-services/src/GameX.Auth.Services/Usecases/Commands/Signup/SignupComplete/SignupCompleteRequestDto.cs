namespace gamex.Auth.Services;

public sealed record SignupCompleteRequestDto(Guid UserId, string OTP);