namespace gamex.Auth.Services;

public sealed record ProfileUpdateRequestDto(){

    public string FirstName {get; init;} 
    public string LastName {get; init;} 
    public string Email {get; init;} 
    public string Mobile {get; init;}  
    public string UserName {get; init;}
    public DateOnly DateOfBirth {get; init;}
    public string City {get; init;}
    public string Country {get; init;}
    public string AvatarName {get; init;}

}