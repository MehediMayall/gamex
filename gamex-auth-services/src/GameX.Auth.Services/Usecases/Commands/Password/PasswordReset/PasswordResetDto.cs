namespace gamex.Auth.Services;

public sealed record PasswordResetDto(Guid UserId, string Password);
