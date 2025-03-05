namespace gamex.Auth.Services;

public sealed record LoggedInResponseDto()
{

    public Guid PlayerId {get; init;}
    public Guid UserId {get; init;}
    public string FirstName {get; init;} 
    public string LastName {get; init;}
    public string Email {get; init;} 
    public string Mobile {get; init;}  
    public string UserName {get; init;}  
    public string Token {get; init;}


    public static LoggedInResponseDto Get(UserRegistrationRequestedDto requestedDto, Guid UserId, string Token) {
        return new() {        
            UserId = UserId,
            FirstName = requestedDto.FirstName,
            LastName = requestedDto.LastName,
            Email = requestedDto.Email,
            Mobile = requestedDto.Mobile,
            UserName = requestedDto.UserName,
            Token = Token
        };
    }
    public static LoggedInResponseDto Get(UserProfileResponseDto player, string Token) {
        return new() {
            UserId = player.UserId,
            PlayerId = player.PlayerId,
            FirstName = player.FirstName,
            LastName = player.LastName,
            Email = player.Email,
            Mobile = player.Mobile,
            UserName = player.UserName,
            Token = Token
        };
    }
}