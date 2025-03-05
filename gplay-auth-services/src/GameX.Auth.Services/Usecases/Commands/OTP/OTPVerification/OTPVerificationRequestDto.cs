namespace gamex.Auth.Services;

public sealed record OTPVerificationRequestDto(Guid UserId, string OTP);
