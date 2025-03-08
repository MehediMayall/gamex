namespace gamex.Auth.Services;

public sealed record UserProfileResponseDto(){

    public Guid UserId {get; init;}
    public Guid PlayerId {get; init;}
    public string FirstName {get; init;}
    public string LastName {get; init;} 
    public string Email {get; init;}
    public string Mobile {get; init;}  
    public string UserName {get; init;}  
    public DateOnly? DateOfBirth {get; init;}
    public string City {get; init;}
    public string Country {get; init;}
    public string AvatarName {get; init;}
    public string ProfileImagename {get; init;}
}