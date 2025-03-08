using System.IdentityModel.Tokens.Jwt;

namespace gamex.Auth.Services;

public static class SignupRequestErrors {
    public static Error NullValue(Guid userid) => new("NullValue", $"Null value provided. Invalid User Id {userid}");
    public static Error JWTConfigNotFound() => new("Not Configured", $"JwtSettings is not found or configured in appsettings.");
    public static Error TokenNotGenerated(JwtSecurityToken jwtToken) => new("NullValue", $"Token generation is failed. {jwtToken.Issuer}");
    public static Error OTPVerificationFailed(string OTP) => new("NullValue", $"OTP verification failed. Invalid OTP {OTP}");

    public static Error OTPGenerationFailed() => new("Not Configured", $"Failed to generate OTP. Please try again.");
    public static Error UserAlreadyExists() =>
        new("User already exists", $"User already exists.");

} 