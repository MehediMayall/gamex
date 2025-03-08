namespace gamex.Auth.Services;


public sealed class LoginErrors : ExceptionBase<LoginRequestDto> {
    public static Error UserNotFoundById(Guid Id) => new("User Not Found", $"Couldn't find user using {nameof(Id)} {Id}. Please try again.");
    public static Error UserNotFoundByEmail(Email Email) => new("User Not Found", $"Couldn't find user using {nameof(Email)} {Email.Value} and given password. Please try again.");
    public static Error UserNotFoundByMobile(Mobile Mobile) => new("User Not Found", $"Couldn't find user using {nameof(Mobile)} {Mobile.Value} and given password. Please try again.");
    public static Error InvalidRequest() => new("Invalid Request", $"Couldn't find either email or password. Please try again.");
    public static Error InvalidPassword() => new("Invalid Password", $"Invalid password. Please try again with correct password.");
    public static Error UserNotFoundByEmailOrMobile(string Mobile, string Email) => 
        new("User Not Found", $"Couldn't find the user using { (string.IsNullOrEmpty(Mobile) ? Email : Mobile) }");

    public static Error UnauthorizedAccessException() =>
        new("Unauthorized Access", $"Unauthorized Access. Please try again with valid credentials.");
}

