namespace gamex.Auth.Contracts;


public sealed record OTPResendRequestedDto(
    string? FirstName,
    string LastName,
    string? Email,
    string? Mobile,
    string OTP
);
