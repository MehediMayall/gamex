namespace gamex.Auth.Services;

public sealed record ProfileUpdateResponseDto(
    string FirstName, 
    string LastName, 
    string Email, 
    string Mobile,
    string UserName,  
    DateOnly? DateOfBirth,  
    string City,
    string Country,
    string AvatarName
){
    public static ProfileUpdateResponseDto Get(Player player, User user) => 
        new(
        player.FirstName, 
        player.LastName, 
        player.Email is null ? "" : player.Email.Value, 
        player.Mobile is null ? "" : player.Mobile.Value, 
        user.UserName, 
        player.DateOfBirth,
        player.City, 
        player.Country, 
        player.AvatarName);
};
