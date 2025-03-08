namespace gamex.Auth.Contracts;

public sealed record UserRegistrationRequestedDto(){

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Mobile { get; init; }
    public DateOnly DateOfBirth { get; init; }  
    public string Password { get; init; }
    public string UserName { get; init; }
    public string City { get; init; } 
    public string Country { get; init; }
    public string AvatarName { get; init; }
    public string OTP { get; init; }

}

