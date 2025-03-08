namespace gamex.Auth.Contracts;

public sealed record UserCreatedDto(
    Guid Userid,
    string FirstName,
    string LastName,
    string Email,
    string Mobile,
    string Password
);