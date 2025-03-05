namespace gamex.Auth.Services;

public sealed record PasswordChangeRequestDto(string CurrentPassword, string NewPassword);
