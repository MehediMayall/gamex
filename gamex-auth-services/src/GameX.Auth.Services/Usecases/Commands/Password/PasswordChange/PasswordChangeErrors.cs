namespace gamex.Auth.Services;


public sealed class PasswordChangeErrors : ExceptionBase<PasswordChangeRequestDto> {
    public static Error InvalidCurrentPassword() => new("Invalid Password", $"Invalid current password. Please try again with correct password.");
    public static Error UserNotFoundByEmailOrMobile(string Mobile, string Email) => 
        new("User Not Found", $"Couldn't find the user using { (string.IsNullOrEmpty(Mobile) ? Email : Mobile) }");

    public static Error UnauthorizedAccessException() =>
        new("Unauthorized Access", $"Unauthorized Access. Please try again with valid credentials.");
}

